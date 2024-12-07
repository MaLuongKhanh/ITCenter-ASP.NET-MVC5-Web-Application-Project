using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace WebApplication.Controllers
{
    public class DefaultController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();
        // GET: Default
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult getStatistic()
        {
            // Get the number of KTV
            var KTV = _db.HumanResources
                .Where(s => s.hide == true)
                .ToList();
            // Get the number of support requests for the current day
            DateTime today = DateTime.Today;
            DateTime endOfDay = today.AddDays(1);

            var supportRequestsToday = _db.SupportHistories
                .Where(s => s.ngaygui >= today && s.ngaygui < endOfDay)
                .ToList();
            // Get the number of devices repaired so far
            var repairedDevicesCount = _db.SupportHistories
                .Where(s => s.trangthai == "bàn giao máy")
                .ToList();

            var model = Tuple.Create(KTV.AsEnumerable(), supportRequestsToday.AsEnumerable(), repairedDevicesCount.AsEnumerable());
            return PartialView(model);
        }
        public ActionResult getKTVList(int page = 1)
        {
            int pageSize = 10;
            var ktv = _db.HumanResources.Where(s => s.hide == true).OrderBy(s => s.hoten);

            int totalItems = ktv.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var ktvPaged = ktv.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return PartialView(ktvPaged);
        }
        private string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult getNews()
        {
            var news = _db.News
                .OrderByDescending(x => x.datebegin)
                .ToList();
            return PartialView(news);
        }
        public ActionResult createSupportRequest()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createSupportRequest(SupportHistory model)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                model.trangthai = "chờ";
                model.hide = true;
                _db.SupportHistories.Add(model);
                _db.SaveChanges();

                message = "Yêu cầu hỗ trợ đã được gửi thành công.";

            }
            else
            {
                // If validation fails, redisplay the form with error messages
                message = "Đã có lỗi xảy ra!";
            }
            TempData["Message"] = message;
            return Redirect("/trang-chu#scrollToForm");

        }
        public ActionResult getTodoList()
        {
            var todos = _db.Todoes.OrderByDescending(x => x.CreatAt).ToList();
            return PartialView(todos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTodo(string content)
        {
            try
            {
                var todo = new Todo
                {
                    Content = content,
                    IsCompleted = false,
                    CreatAt = DateTime.Now
                };
                _db.Todoes.Add(todo);
                _db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleTodo(int id, bool isCompleted)
        {
            try
            {
                var todo = _db.Todoes.Find(id);
                if (todo != null)
                {
                    todo.IsCompleted = isCompleted;
                    _db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = "Todo not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTodo(int id)
        {
            try
            {
                var todo = _db.Todoes.Find(id);
                if (todo != null)
                {
                    _db.Todoes.Remove(todo);
                    _db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = "Todo not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateSupportRequestJson()
        {
            try
            {
                // Đọc JSON từ request body
                Request.InputStream.Position = 0;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    var jsonString = reader.ReadToEnd();
                    var model = JsonConvert.DeserializeObject<SupportHistory>(jsonString);

                    if (model == null)
                    {
                        return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                    }

                    // Thiết lập các giá trị mặc định
                    model.trangthai = "chờ";
                    model.hide = true;
                    model.ngaygui = DateTime.Now;

                    if (string.IsNullOrEmpty(model.hoten) ||
                        string.IsNullOrEmpty(model.maKH) ||
                        string.IsNullOrEmpty(model.email) ||
                        string.IsNullOrEmpty(model.sdt) ||
                        string.IsNullOrEmpty(model.tenmay) ||
                        string.IsNullOrEmpty(model.loaihotro))
                    {
                        return Json(new { success = false, message = "Vui lòng điền đầy đủ thông tin bắt buộc." });
                    }

                    // Kiểm tra định dạng email
                    if (!IsValidEmail(model.email))
                    {
                        return Json(new { success = false, message = "Email không hợp lệ." });
                    }

                    // Kiểm tra định dạng số điện thoại
                    if (!IsValidPhoneNumber(model.sdt))
                    {
                        return Json(new { success = false, message = "Số điện thoại không hợp lệ." });
                    }

                    _db.SupportHistories.Add(model);
                    _db.SaveChanges();

                    return Json(new { success = true, message = "Yêu cầu hỗ trợ đã được gửi thành công." });
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return Json(new { success = false, message = "Đã có lỗi xảy ra khi xử lý yêu cầu." });
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra số điện thoại VN (10 hoặc 11 số)
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^(0[0-9]{9,10})$");
        }

        public JsonResult GetNewsPaged(int page = 1, int pageSize = 5)
        {
            try
            {
                var allNews = _db.News.Where(s => s.hide == true)
                                     .OrderByDescending(n => n.datebegin);

                var totalItems = allNews.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var newsForPage = allNews.Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                var html = RenderNewsListToString(newsForPage);

                return Json(new
                {
                    success = true,
                    html = html,
                    currentPage = page,
                    totalPages = totalPages
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private string RenderNewsListToString(List<News> news)
        {
            var sb = new StringBuilder();
            foreach (var item in news)
            {
                var encodedContent = HttpUtility.JavaScriptStringEncode(item.content);
                var encodedTitle = HttpUtility.JavaScriptStringEncode(item.ten);

                sb.Append($@"
                    <div class='preview-item news-item' onclick='showNewsDetail({item.news_id}, ""{encodedTitle}"", ""{encodedContent}"", ""{item.datebegin:dd/MM/yyyy}"")'>
                        <div class='preview-thumbnail'>");

                if (!string.IsNullOrEmpty(item.thumbnail))
                {
                    sb.Append($@"<img src='/Areas/admin/Content/upload/img/news/{item.thumbnail}' alt='{item.ten}'/>");
                }
                else
                {
                    sb.Append(@"<div class='preview-icon bg-info'><i class='mdi mdi-file-document'></i></div>");
                }

                sb.Append($@"
                        </div>
                        <div class='preview-item-content'>
                            <h6 class='preview-subject'>{item.ten}</h6>
                            <p class='text-muted mb-0'>{item.datebegin:dd/MM/yyyy}</p>
                        </div>
                    </div>");
            }
            return sb.ToString();
        }
    }

    public class JsonModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var jsonString = new StreamReader(request.InputStream).ReadToEnd();
            return JsonConvert.DeserializeObject(jsonString, bindingContext.ModelType);
        }
    }
}