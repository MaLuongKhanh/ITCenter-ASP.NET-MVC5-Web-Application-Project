using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using BCrypt.Net;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Security.Principal;

namespace WebApplication.Controllers
{
    
    public class LoginController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();

        public string myEmailUsername = "maluongkhanh8@gmail.com";
        public string myEmailPassword = "qzla rfxt ogbj ykyq";
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Account account)
        {
            ModelState.Remove("username");
            ModelState.Remove("password");
            ModelState.Remove("email");
            ModelState.Remove("mssv");

            var user = _db.Accounts.FirstOrDefault(u => u.username == account.username);

            if (user != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(account.password, user.password);

                if (isPasswordValid)
                {
                    FormsAuthentication.SetAuthCookie(account.username, false);
                    Session["MSSV"] = user.mssv;
                    Session["Username"] = user.username;
                    Session["Role"] = user.role;

                    switch (user.role?.ToLower())
                    {
                        case "admin":
                            return Redirect("/admin/");
                        case "customer":
                            return Redirect("/");
                        default:
                            return Redirect("/trang-chu");
                    }
                }
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View(account);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var humanResource = new HumanResource
                    {
                        mssv = account.mssv,
                        avatar = "default-avatar.jpg",
                        trangthai = "Đang hoạt động",
                        ngaysinh = null,
                        hide = true
                    };
                    _db.HumanResources.Add(humanResource);

                    account.password = BCrypt.Net.BCrypt.HashPassword(account.password);
                    account.datebegin = DateTime.Now;
                    account.role = "customer";
                    _db.Accounts.Add(account);
                    _db.SaveChanges();
                    return Redirect("/dang-nhap");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đăng ký: " + ex.Message);
                }
            }
            return View(account);

        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            string message = "";
            bool status = false;
            var account = await _db.Accounts.Where(m => m.email == email).FirstOrDefaultAsync();
            if (account != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.email, resetCode, "reset-password");
                account.PasswordResetToken = resetCode;
                _db.Configuration.ValidateOnSaveEnabled = false;
                _db.SaveChanges();
                message = "Link reset mật khẩu đã được gửi đến email của bạn!";
            }
            else
            {
                message = "Email không tồn tại trong hệ thống!";
            }
            ViewBag.message = message;
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            var user = _db.Accounts.Where(m => m.PasswordResetToken == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.Token = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var user = _db.Accounts.Where(m => m.PasswordResetToken == model.Token).FirstOrDefault();
                if (user != null)
                {
                    user.password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                    user.PasswordResetToken = "";
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.SaveChanges();
                    message = "Mật khẩu đã được cập nhật";
                }
                else
                {
                    message = "Token không đúng hoặc quá hạn";
                }
            }
            else
            {
                message = "Something's wrong";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [NonAction]
        public void SendVerificationLinkEmail(string email, string code, string emailFor)
        {
            var verifyUrl = "/"+emailFor+"/"+code;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress(myEmailUsername, "ITCenter");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = myEmailPassword;

            string subject = "";
            string body = "";
            if (emailFor == "reset-password")
            {
                subject = "Reset mật khẩu!";
                body = "<br/>click link bên dưới để đổi mật khẩu<br/><a href='" + link + "'>Reset</a>";

            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            smtp.Send(message);
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu mới không khớp.");
                return View();
            }

            var username = Session["Username"].ToString();
            var user = _db.Accounts.FirstOrDefault(u => u.username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user.password))
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng.");
                return View();
            }

            try
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Xóa tất cả session
            Session.Abandon(); // Hủy session hiện tại
            FormsAuthentication.SignOut(); // Đăng xuất authentication
            return Redirect("/dang-nhap"); // Chuyển về trang đăng nhập
        }
    }
}