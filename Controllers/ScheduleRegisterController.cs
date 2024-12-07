using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ScheduleRegisterController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();

        // GET: ScheduleRegister
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            var scheduleDetails = _db.ScheduleDetails
                .Where(sd => sd.hide == true)
                .ToList();

            var schedules = _db.Schedules
                .Where(s => s.hide == true)
                .ToList();

            var viewModel = new ScheduleViewModel
            {
                ScheduleDetails = scheduleDetails,
                Schedules = schedules
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string mssv, int catruc, int ngaytrongtuan)
        {
            // Kiểm tra số người đã đăng ký trong ca trực này
            var countInShift = _db.Schedules
                .Count(s => s.catruc == catruc && s.ngaytrongtuan == ngaytrongtuan);

            if (countInShift >= 6)
            {
                // Trả về lỗi nếu số lượng kỹ thuật viên đã đạt tối đa
                TempData["Error"] = "Ca trực này đã đủ 6 kỹ thuật viên. Vui lòng chọn ca trực khác.";
                return Redirect("/dang-ky-ca-truc");
            }

            // Tiến hành đăng ký nếu chưa đạt giới hạn
            var newSchedule = new Schedule
            {
                mssv = Session["MSSV"]?.ToString(),
                catruc = catruc,
                ngaytrongtuan = ngaytrongtuan,
                ngaydangki = DateTime.Now,
                hide = true
            };

            _db.Schedules.Add(newSchedule);
            _db.SaveChanges();

            // Thông báo thành công
            TempData["Success"] = "Đăng ký ca trực thành công!";
            return Redirect("/dang-ky-ca-truc");
        }
        [HttpPost]
        public JsonResult DeleteConfirmed(string scheduleIds)
        {
            try
            {
                if (string.IsNullOrEmpty(scheduleIds))
                {
                    return Json(new { success = false, message = "Không có lịch trực nào được chọn." });
                }

                var ids = scheduleIds.Split(',').Select(int.Parse).ToList();
        
                foreach (var id in ids)
                {
                    var schedule = _db.Schedules.Find(id);
                    if (schedule != null)
                    {
                        schedule.hide = false;
                    }
                }
                _db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xử lý yêu cầu." });
            }   
        }

    }
}
