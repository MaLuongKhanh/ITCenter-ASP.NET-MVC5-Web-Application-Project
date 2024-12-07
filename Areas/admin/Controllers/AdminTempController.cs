using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using BCrypt.Net;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Security.Principal;

namespace WebApplication.Areas.Admin.Controllers
{
    public class AdminTempController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult getHeader()
        {
            var header = db.Headers.FirstOrDefault();
            return PartialView("_Header", header);
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