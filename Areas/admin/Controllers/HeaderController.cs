using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Areas.admin.Controllers
{
    public class HeaderController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index()
        {
            var header = db.Headers.FirstOrDefault();
            return View(header);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Header header, HttpPostedFileBase logoFile, HttpPostedFileBase faviconFile)
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
                TempData["SuccessMessage"] = "Cập nhật thành công!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Không tìm thấy header để cập nhật!";
            return View("Index", header);
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