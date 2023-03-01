using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.Admin.Controllers
{
    [Authorize(Roles = "quanly")]
    public class KhoaController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/Khoa
        public ActionResult list_khoa()
        {
            var k = from s in db.KHOAs select s;
            return View(k);
        }

        // GET: Admin/Khoa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Khoa/Create
        public ActionResult create_khoa()
        {
            return View();
        }

        // POST: Admin/Khoa/Create
        [HttpPost, ActionName("create_khoa")]
        [ValidateInput(false)]
        public ActionResult Create(KHOA k)
        {
            if (ModelState.IsValid)
            {
                db.KHOAs.Add(k);
                db.SaveChanges();
                return RedirectToAction("list_khoa");
            }

            return View(k);
        }

        // GET: Admin/Khoa/Edit/5
        [HttpGet]
        public ActionResult edit_khoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA k = db.KHOAs.Find(id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        // POST: Admin/Khoa/Edit/5
        [HttpPost, ActionName("edit_khoa")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {

            var k = db.KHOAs.Find(id);
            if (TryUpdateModel(k, "", new string[] { "K_MA", "K_TEN", }))
            {


                // TODO: Add update logic here
                db.Entry(k).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_khoa");

            }
            return View(k);
        }

        // GET: Admin/Khoa/Delete/5
        [HttpGet]
        public ActionResult delete_khoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA k = db.KHOAs.Find(id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        // POST: Admin/Khoa/Delete/5
        [HttpPost, ActionName("delete_khoa")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            KHOA k = db.KHOAs.SingleOrDefault(n => n.K_MA == id);

            db.KHOAs.Remove(k);
            db.SaveChanges();
            return RedirectToAction("list_khoa");
        }
    }
}
