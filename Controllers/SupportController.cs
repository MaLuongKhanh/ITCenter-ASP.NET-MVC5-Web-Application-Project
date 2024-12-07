using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    public class SupportController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();
        private int PageSize = 10;

        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult getSupportList(string searchTerm = "", int page = 1)
        {
            var query = _db.SupportHistories.Where(s => s.hide == true);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(s => 
                    s.hoten.ToLower().Contains(searchTerm) ||
                    s.maKH.ToLower().Contains(searchTerm) ||
                    s.loaihotro.ToLower().Contains(searchTerm)
                );
            }

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            
            var supportList = query
                .OrderByDescending(s => s.ngaygui)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return PartialView(supportList);
        }

        [HttpPost]
        public ActionResult DeleteSelected(List<int> ids)
        {
            try
            {
                var itemsToDelete = _db.SupportHistories.Where(x => ids.Contains(x.support_id)).ToList();
                foreach (var item in itemsToDelete)
                {
                    item.hide = false; // Soft delete by setting hide flag
                }
                _db.SaveChanges();
                return Json(new { success = true, message = "Xóa thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa!" });
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            try
            {
                var support = _db.SupportHistories.Find(id);
                if (support != null)
                {
                    support.trangthai = status;
                    support.mssv = Session["MSSV"]?.ToString(); // Lấy MSSV từ Session
                    _db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật trạng thái thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy yêu cầu hỗ trợ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật!" });
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var support = _db.SupportHistories.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupportHistory model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var support = _db.SupportHistories.Find(model.support_id);
                    if (support != null)
                    {
                        support.hoten = model.hoten;
                        support.maKH = model.maKH;
                        support.tenmay = model.tenmay;
                        support.loaihotro = model.loaihotro;
                        support.sdt = model.sdt;
                        support.email = model.email;
                        // Không cập nhật trạng thái
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật.");
                }
            }
            return View(model);
        }
    }
}