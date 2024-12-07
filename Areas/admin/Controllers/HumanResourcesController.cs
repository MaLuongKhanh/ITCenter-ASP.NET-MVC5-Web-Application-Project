using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Areas.admin.Models;

namespace WebApplication.Areas.admin.Controllers
{
    public class HumanResourcesController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var technicians = (from h in db.HumanResources
                              join a in db.Accounts on h.mssv equals a.mssv
                              orderby h.mssv
                              select new HumanResourceViewModel
                              {
                                  Avatar = h.avatar,
                                  Hoten = h.hoten,
                                  Mssv = h.mssv,
                                  Trangthai = h.trangthai,
                                  Role = a.role
                              })
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = Math.Ceiling((double)db.HumanResources.Count() / pageSize);
            
            return View(technicians);
        }

        [HttpPost]
        public JsonResult ToggleStatus(string mssv, bool isActive)
        {
            try
            {
                var user = db.HumanResources.FirstOrDefault(x => x.mssv == mssv);
                if (user != null)
                {
                    user.trangthai = isActive ? "Đang hoạt động" : "Không hoạt động";
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

        [HttpPost]
        public JsonResult UpdateRole(string mssv, string role)
        {
            try
            {
                var account = db.Accounts.FirstOrDefault(x => x.mssv == mssv);
                if (account != null)
                {
                    account.role = role;
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
    }
} 