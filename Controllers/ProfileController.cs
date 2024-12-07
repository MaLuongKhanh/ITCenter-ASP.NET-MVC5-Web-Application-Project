using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        private ITCenterEntities1 _db = new ITCenterEntities1();

        // GET: Profile
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }

            var profile = _db.HumanResources.FirstOrDefault(x => x.mssv == mssv);
            var account = _db.Accounts.FirstOrDefault(x => x.mssv == mssv);

            ViewBag.Email = account?.email;

            return View(profile);
        }

        [HttpPost]
        public ActionResult UpdateProfile(HumanResource model, string email)
        {
            try
            {
                var profile = _db.HumanResources.Find(model.mssv);
                if (profile == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng" });
                }

                string currentMssv = Session["MSSV"]?.ToString();
                if (string.IsNullOrEmpty(currentMssv) || profile.mssv != currentMssv)
                {
                    return Json(new { success = false, message = "Bạn không có quyền cập nhật thông tin này" });
                }

                // Cập nhật HumanResource
                profile.hoten = model.hoten;
                profile.ngaysinh = model.ngaysinh;
                profile.facebookURL = model.facebookURL;
                profile.githubURL = model.githubURL;
                profile.sodienthoai = model.sodienthoai;

                // Cập nhật email trong Account
                var account = _db.Accounts.FirstOrDefault(a => a.mssv == currentMssv);
                if (account != null)
                {
                    account.email = email;
                }

                _db.SaveChanges();
                return Json(new { success = true, message = "Cập nhật thông tin thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật thông tin" });
            }
        }

        [HttpPost]
        public ActionResult UploadAvatar()
        {
            try
            {
                var file = Request.Files[0];
                string mssv = Session["MSSV"]?.ToString();

                if (file != null && file.ContentLength > 0)
                {
                    var profile = _db.HumanResources.Find(mssv);
                    if (profile == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin người dùng" });
                    }

                    // Validate file type
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        return Json(new { success = false, message = "Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif)" });
                    }

                    // Create images directory if it doesn't exist
                    string uploadDir = Server.MapPath("~/Content/assets/images/faces-clipart");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    // Delete old avatar if exists
                    if (!string.IsNullOrEmpty(profile.avatar))
                    {
                        string oldAvatarPath = Path.Combine(uploadDir, profile.avatar);
                        if (System.IO.File.Exists(oldAvatarPath))
                        {
                            System.IO.File.Delete(oldAvatarPath);
                        }
                    }

                    // Save new avatar
                    string newFileName = $"{mssv}{extension}";
                    string path = Path.Combine(uploadDir, newFileName);
                    file.SaveAs(path);

                    // Update database
                    profile.avatar = newFileName;
                    _db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        fileName = newFileName,
                        message = "Cập nhật ảnh đại diện thành công"
                    });
                }

                return Json(new { success = false, message = "Không có file được chọn" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi tải ảnh lên" });
            }
        }
    }
}