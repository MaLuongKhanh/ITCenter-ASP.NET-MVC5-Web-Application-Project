using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AttendanceController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();
        // GET: Attendance
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            var KTVList = _db.HumanResources.ToList();
            return View(KTVList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime ngaytruc, int catruc)
        {
            string message = "";
            bool hasAttendanceData = false;

            foreach (var KTV in _db.HumanResources.Where(s => s.hide == true))
            {
                string mssv = KTV.mssv.ToString();
                string attendanceStatus = Request.Form["attendance[" + mssv + "]"];

                if (!string.IsNullOrEmpty(attendanceStatus))
                {
                    hasAttendanceData = true;
                    var attendanceRecord = new Attendance
                    {
                        mssv = mssv,
                        diemdanh = attendanceStatus,
                        ngaytruc = ngaytruc,
                        catruc = catruc,
                        meta = "",
                        hide = true,
                    };
                    message = mssv + attendanceStatus;
                    _db.Attendances.Add(attendanceRecord);
                }
            }

            if (hasAttendanceData)
            {
                _db.SaveChanges();
                TempData["Message"] = "Điểm danh thành công!";
            }
            else
            {
                TempData["Message"] = "Error: No attendance data received";
            }

            return Redirect("diem-danh-ca-truc");
        }

    }
}