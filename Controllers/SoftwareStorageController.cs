using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class SoftwareStorageController : Controller
    {
        private readonly ITCenterEntities1 _db;

        public SoftwareStorageController()
        {
            _db = new ITCenterEntities1();
        }

        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult getSoftwareList(int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _db.SoftwareStorages.OrderByDescending(s => s.ngaydang);
                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                
                var softwareList = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                
                return PartialView(softwareList);
            }
            catch (Exception ex)
            {
                return PartialView(new List<SoftwareStorage>());
            }
        }

        [HttpPost]
        public JsonResult Create(SoftwareStorage software)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { 
                        success = false, 
                        message = string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage))
                    });
                }

                software.ten = HttpUtility.HtmlEncode(software.ten);
                software.mota = HttpUtility.HtmlEncode(software.mota);
                software.link = HttpUtility.HtmlEncode(software.link);
                
                software.ngaydang = DateTime.Now;
                software.mssv = Session["MSSV"]?.ToString();

                _db.SoftwareStorages.Add(software);
                _db.SaveChanges();
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi thêm phần mềm" });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var software = _db.SoftwareStorages.Find(id);
                if (software == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy phần mềm" });
                }

                if (software.mssv != Session["MSSV"]?.ToString())
                {
                    return Json(new { success = false, message = "Bạn không có quyền xóa phần mềm này" });
                }

                _db.SoftwareStorages.Remove(software);
                _db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa phần mềm" });
            }
        }

        [HttpPost]
        public JsonResult Edit(SoftwareStorage software)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { 
                        success = false, 
                        message = string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage))
                    });
                }

                var existingSoftware = _db.SoftwareStorages.Find(software.software_id);
                if (existingSoftware == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy phần mềm" });
                }

                if (existingSoftware.mssv != Session["MSSV"]?.ToString())
                {
                    return Json(new { success = false, message = "Bạn không có quyền chỉnh sửa phần mềm này" });
                }

                existingSoftware.ten = HttpUtility.HtmlEncode(software.ten);
                existingSoftware.mota = HttpUtility.HtmlEncode(software.mota);
                existingSoftware.link = HttpUtility.HtmlEncode(software.link);

                _db.SaveChanges();
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật phần mềm" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}