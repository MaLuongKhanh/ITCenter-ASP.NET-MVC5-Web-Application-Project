using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Collections.Generic;

namespace WebApplication.Areas.admin.Controllers
{
    public class LandingManagementController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index()
        {
            var landingContent = db.LandingContents.FirstOrDefault();
            if (landingContent == null)
            {
                landingContent = new LandingContent
                {
                    HeaderTitle = "Welcome to IT-Center",
                    HeaderSubtitle = "Trung tâm hỗ trợ kỹ thuật",
                    AboutTitle = "Về chúng tôi",
                    AboutContent = "Nội dung mặc định về trung tâm",
                    ServicesTitle = "Dịch vụ"
                };
                db.LandingContents.Add(landingContent);
                db.SaveChanges();
            }
            return View(landingContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveContent(LandingContent model, 
            HttpPostedFileBase headerBgImage,
            HttpPostedFileBase bannerImage,
            HttpPostedFileBase service1Image,
            HttpPostedFileBase service2Image,
            HttpPostedFileBase service3Image,
            HttpPostedFileBase[] spotlightImages,
            HttpPostedFileBase featureImage)
        {
            if (ModelState.IsValid)
            {
                string uploadPath = Server.MapPath("~/Content/landingPage/images/landing/");
                var content = db.LandingContents.Find(model.Id);
                
                if (content == null)
                {
                    content = new LandingContent();
                    db.LandingContents.Add(content);
                }

                // Cập nhật các trường text
                content.HeaderTitle = model.HeaderTitle;
                content.HeaderSubtitle = model.HeaderSubtitle;
                content.AboutTitle = model.AboutTitle;
                content.AboutContent = model.AboutContent;
                content.ServicesTitle = model.ServicesTitle;
                content.Service1Title = model.Service1Title;
                content.Service2Title = model.Service2Title;
                content.Service3Title = model.Service3Title;
                content.SpotlightTitle = model.SpotlightTitle;
                content.SpotlightSubtitle = model.SpotlightSubtitle;

                // Xử lý từng file ảnh
                if (headerBgImage != null && headerBgImage.ContentLength > 0)
                {
                    string headerBgFileName = Path.GetFileName(headerBgImage.FileName);
                    headerBgImage.SaveAs(Path.Combine(uploadPath, headerBgFileName));
                    content.HeaderBackground = headerBgFileName;
                }

                if (bannerImage != null && bannerImage.ContentLength > 0)
                {
                    string bannerFileName = Path.GetFileName(bannerImage.FileName);
                    bannerImage.SaveAs(Path.Combine(uploadPath, bannerFileName));
                    content.BannerImage = bannerFileName;
                }

                if (service1Image != null && service1Image.ContentLength > 0)
                {
                    string service1FileName = Path.GetFileName(service1Image.FileName);
                    service1Image.SaveAs(Path.Combine(uploadPath, service1FileName));
                    content.Service1Image = service1FileName;
                }

                if (service2Image != null && service2Image.ContentLength > 0)
                {
                    string service2FileName = Path.GetFileName(service2Image.FileName);
                    service2Image.SaveAs(Path.Combine(uploadPath, service2FileName));
                    content.Service2Image = service2FileName;
                }

                if (service3Image != null && service3Image.ContentLength > 0)
                {
                    string service3FileName = Path.GetFileName(service3Image.FileName);
                    service3Image.SaveAs(Path.Combine(uploadPath, service3FileName));
                    content.Service3Image = service3FileName;
                }

                if (spotlightImages != null && spotlightImages.Any(i => i != null && i.ContentLength > 0))
                {
                    var newSpotlightFileNames = new List<string>();
                    
                    // Xử lý các ảnh mới
                    foreach (var image in spotlightImages.Where(i => i != null && i.ContentLength > 0))
                    {
                        string spotlightFileName = Path.GetFileName(image.FileName);
                        var fullPath = Path.Combine(uploadPath, spotlightFileName);
                        image.SaveAs(fullPath);
                        newSpotlightFileNames.Add(spotlightFileName);
                    }
                    
                    // Cập nhật database với chỉ danh sách ảnh mới
                    content.SpotlightImages = string.Join(",", newSpotlightFileNames);
                }

                if (featureImage != null && featureImage.ContentLength > 0)
                {
                    string featureFileName = Path.GetFileName(featureImage.FileName);
                    featureImage.SaveAs(Path.Combine(uploadPath, featureFileName));
                    content.FeatureImage = featureFileName;
                }

                db.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật nội dung thành công!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void LogDebug(string message)
        {
            string logPath = Server.MapPath("~/App_Data/debug.log");
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
            System.IO.File.AppendAllText(logPath, logMessage);
        }
    }
}