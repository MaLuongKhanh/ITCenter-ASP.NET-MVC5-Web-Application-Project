using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;


namespace WebApplication.Areas.admin.Controllers
{
    public class DefaultController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();
        public ActionResult Index()
        {
            var humanResources = db.HumanResources.Where(s => s.hide == true)
                .ToList();

            DateTime today = DateTime.Today;
            DateTime endOfDay = today.AddDays(1);
            var todaySupport = db.SupportHistories
                .Where(s => s.ngaygui >= today && s.ngaygui < endOfDay)
                .ToList();
            var completedSupport = db.SupportHistories.Where(s => s.trangthai == "bàn giao máy").ToList();
            
            var model = Tuple.Create(humanResources.AsEnumerable(), todaySupport.AsEnumerable(), completedSupport.AsEnumerable());
            
            return View(model);
        }
    }
}