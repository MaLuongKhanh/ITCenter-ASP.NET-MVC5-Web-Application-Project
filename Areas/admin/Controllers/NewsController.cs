using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Data.Entity.Validation;


namespace WebApplication.Areas.admin.Controllers
{
    public class NewsController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getNewsList(string search = "", int page = 1)
        {
            int pageSize = 10;
            var query = db.News.OrderByDescending(n => n.datebegin).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n => n.ten.Contains(search));
            }

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            
            var paginatedNews = query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            
            return PartialView(paginatedNews);
        }

        public ActionResult NewsForm(int? id)
        {
            if (id.HasValue)
            {
                var news = db.News.Find(id);
                if (news == null) return HttpNotFound();
                return PartialView(news);
            }
            return PartialView(new News());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult SaveNews(News news, HttpPostedFileBase thumbnailFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (thumbnailFile != null && thumbnailFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(thumbnailFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/admin/Content/upload/img/news"), fileName);
                        thumbnailFile.SaveAs(path);
                        news.thumbnail = fileName;
                    }

                    news.datebegin = DateTime.Now;
                    news.hide = news.hide.GetValueOrDefault();

                    if (news.news_id > 0)
                    {
                        db.Entry(news).State = EntityState.Modified;
                    }
                    else
                    {
                        db.News.Add(news);
                    }
                    
                    db.SaveChanges();
                    return Json(new { success = true, message = "Lưu thành công!" });
                }
                
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                
                return Json(new { success = false, message = "Dữ liệu không hợp lệ", errors = errors });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"Lỗi: {x.PropertyName} - {x.ErrorMessage}")
                    .ToList();

                return Json(new { 
                    success = false, 
                    message = "Lỗi xác thực dữ liệu", 
                    errors = errorMessages 
                });
            }
            catch (Exception ex)
            {
                return Json(new { 
                    success = false, 
                    message = "Có lỗi xảy ra", 
                    error = ex.Message,
                    stackTrace = ex.StackTrace 
                });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var news = db.News.Find(id);
                if (news != null)
                {
                    db.News.Remove(news);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Không tìm thấy tin tức" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
} 