using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
namespace WebApplication.Areas.admin.Controllers
{
    public class menusController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();
        // GET: admin/menus
        public ActionResult Index()
        {
            return View(db.Menus.ToList());
        }
        // GET: admin/menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: admin/menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: admin/menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "menu_id,tenmenu,link,meta,hide,order,icon,datebegin")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.datebegin = DateTime.Now;
                if (!string.IsNullOrEmpty(menu.icon) && !menu.icon.StartsWith("mdi mdi-"))
                {
                    ModelState.AddModelError("icon", "Icon không hợp lệ");
                    return View(menu);
                }
                
                db.Entry(menu).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu: " + ex.Message);
                }
            }
            return View(menu);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Menu menu = db.Menus.Find(id);
                if (menu != null)
                {
                    db.Menus.Remove(menu);
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
