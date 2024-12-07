using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AchievementController : Controller
    {
        ITCenterEntities1 _db = new ITCenterEntities1();
        // GET: Achievement
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult getKTVMostSupport()
        {
            var KTVMostSupport = _db.SupportHistories
                .Join(_db.HumanResources,
                      sh => sh.mssv,
                      hr => hr.mssv,
                      (sh, hr) => new { hr.hoten, hr.mssv, sh.trangthai }) // Join hai bảng
                .Where(x => x.trangthai == "bàn giao máy") // Lọc theo trạng thái
                .GroupBy(x => new { x.hoten, x.mssv }) // Nhóm theo mssv và hoten
                .Select(g => new KTVAchievementViewModel
        {
                    Hoten = g.Key.hoten,
                    Mssv = g.Key.mssv,
                    TotalCount = g.Count() // Tổng số hỗ trợ
        })
                .OrderByDescending(x => x.TotalCount) // Sắp xếp theo tổng số hỗ trợ giảm dần
                .Take(5) // Lấy top 5
                .ToList();

            return PartialView(KTVMostSupport);
        }


        public ActionResult getKTVMostAttendance()
        {
            var KTVMostAttendance = _db.Attendances
             .Join(_db.HumanResources,
                   a => a.mssv,
                   hr => hr.mssv,
                   (a, hr) => new { hr.hoten, hr.mssv, a.diemdanh }) // Join hai bảng
             .Where(x => x.diemdanh == "có") // Lọc theo trạng thái
             .GroupBy(x => new { x.hoten, x.mssv }) // Nhóm theo mssv và hoten
             .Select(g => new KTVAchievementViewModel
             {
                 Hoten = g.Key.hoten,
                 Mssv = g.Key.mssv,
                 TotalCount = g.Count() // Tổng số buổi trực
            })
             .OrderByDescending(x => x.TotalCount)
             .Take(5)
             .ToList();
            return PartialView(KTVMostAttendance);
        }
    }
}