using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        ITCenterEntities1 _db = new ITCenterEntities1();
        public ActionResult Index()
        {
            string mssv = Session["MSSV"]?.ToString();
            if (string.IsNullOrEmpty(mssv))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult getMenu()
        {
            string mssv = Session["MSSV"]?.ToString();
            
            // Get user info
            var user = _db.HumanResources.FirstOrDefault(x => x.mssv == mssv);
            var account = _db.Accounts.FirstOrDefault(x => x.mssv == mssv);
            
            ViewBag.UserName = user?.hoten ?? "Guest";
            ViewBag.UserRole = (account?.role ?? "customer").ToUpper();
            ViewBag.UserAvatar = user?.avatar != null 
                ? $"/Content/assets/images/faces-clipart/{user.avatar}" 
                : "/Content/assets/images/faces-clipart/default-avatar.jpg";
            
            // Get base menu query
            var menuQuery = _db.Menus.Where(i => i.hide == true);

            // Filter menus based on account role
            if (account != null)
            {
                switch (account.role?.ToLower())
                {
                    case "customer":
                        menuQuery = menuQuery.Where(m => !new[] { "trang-chu", "diem-danh-ca-truc", "kho-phan-mem" }.Contains(m.meta));
                        break;
                    case "member":
                        menuQuery = menuQuery.Where(m => !new[] { "diem-danh-ca-truc", "kho-phan-mem" }.Contains(m.meta));
                        break;
                    default:
                        break;
                }
            }

            // Apply ordering after all filters
            var menus = menuQuery.OrderBy(i => i.order).ToList();

            return PartialView(menus);
        }
        public ActionResult getNavbar()
        {
            var navbar = _db.Navbars.Where(s => s.hide == true).ToList();
            var header = _db.Headers.FirstOrDefault();
            ViewBag.Header = header;
            return PartialView("getNavbar", navbar);
        }
        public ActionResult getFooter()
        {
            var footer = _db.Footers
                .ToList();
            return PartialView(footer);
        }
        public ActionResult getHeader()
        {
            var header = _db.Headers.FirstOrDefault();
            ViewBag.Header = header;
            return PartialView("_Header");
        }
    }
}