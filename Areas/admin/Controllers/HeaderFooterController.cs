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

using System.IO;

namespace WebApplication.Areas.admin.Controllers
{
    public class HeaderFooterController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index()
        {
            ViewBag.Header = db.Headers.FirstOrDefault();
            ViewBag.Footers = db.Footers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHeader(Header header, HttpPostedFileBase logoFile, HttpPostedFileBase faviconFile)
        {
            var headerToUpdate = db.Headers.Find(header.id);
            
            if (headerToUpdate != null)
            {
                // Xử lý upload logo
                if (logoFile != null && logoFile.ContentLength > 0)
                {
                    string logoFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(logoFile.FileName);
                    string logoPath = Path.Combine(Server.MapPath("~/Areas/admin/Content/images/"), logoFileName);
                    
                    // Xóa file cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(headerToUpdate.logo))
                    {
                        string oldLogoPath = Path.Combine(Server.MapPath("~/Areas/admin/Content/images/"), headerToUpdate.logo);
                        if (System.IO.File.Exists(oldLogoPath))
                        {
                            System.IO.File.Delete(oldLogoPath);
                        }
                    }
                    
                    logoFile.SaveAs(logoPath);
                    headerToUpdate.logo = logoFileName;
                }

                // Xử lý upload favicon
                if (faviconFile != null && faviconFile.ContentLength > 0)
                {
                    string faviconFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(faviconFile.FileName);
                    string faviconPath = Path.Combine(Server.MapPath("~/Areas/admin/Content/images/"), faviconFileName);
                    
                    // Xóa file cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(headerToUpdate.favicon))
                    {
                        string oldFaviconPath = Path.Combine(Server.MapPath("~/Areas/admin/Content/images/"), headerToUpdate.favicon);
                        if (System.IO.File.Exists(oldFaviconPath))
                        {
                            System.IO.File.Delete(oldFaviconPath);
                        }
                    }
                    
                    faviconFile.SaveAs(faviconPath);
                    headerToUpdate.favicon = faviconFileName;
                }

                headerToUpdate.websiteName = header.websiteName;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật Header thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy Header để cập nhật!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult EditFooter(Footer footer)
        {
            try
            {
                var footerToUpdate = db.Footers.Find(footer.footer_id);
                
                if (footerToUpdate != null)
                {
                    footerToUpdate.contentleft = footer.contentleft;
                    footerToUpdate.contentright = footer.contentright;
                    footerToUpdate.link = footer.link;
                    footerToUpdate.hide = footer.hide;
                    
                    db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật Footer thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy Footer để cập nhật!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var footer = db.Footers.Find(id);
                if (footer != null)
                {
                    db.Footers.Remove(footer);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public ActionResult Edit(int id)
        {
            var footer = db.Footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
} 