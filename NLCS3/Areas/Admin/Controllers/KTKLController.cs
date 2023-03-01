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
    public class KTKLController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/KTKL
        public ActionResult list_ktkl()
        {
            var kt = from s in db.KHENTHUONGKYLUATs select s;
            return View(kt);
        }

        // GET: Admin/KTKL/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/KTKL/Create
        public ActionResult create_ktkl()
        {
          
            List<VIENCHUC> cate = db.VIENCHUCs.ToList();
            SelectList cateList = new SelectList(cate, "VC_MA", "VC_HOTEN");
            ViewBag.CateList = cateList;
            return View();
        }

        // POST: Admin/KTKL/Create
        [HttpPost, ActionName("create_ktkl")]
        [ValidateInput(false)]
        public ActionResult Create(KHENTHUONGKYLUAT kt, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.KHENTHUONGKYLUATs.Add(kt);
                db.SaveChanges();
                return RedirectToAction("list_ktkl");
            }

            return View(kt);
        }

        // GET: Admin/KTKL/Edit/5
        [HttpGet]
        public ActionResult edit_ktkl(int? id)
        {
            List<VIENCHUC> cate = db.VIENCHUCs.ToList();
            SelectList cateList = new SelectList(cate, "VC_MA", "VC_HOTEN");
            ViewBag.CateList = cateList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHENTHUONGKYLUAT kt = db.KHENTHUONGKYLUATs.Find(id);
            if (kt == null)
            {
                return HttpNotFound();
            }
            return View(kt);
        }

        // POST: Admin/KTKL/Edit/5
        [HttpPost, ActionName("edit_ktkl")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var kt = db.KHENTHUONGKYLUATs.Find(id);
            if (TryUpdateModel(kt, "", new string[] { "KT_MA", "VC_MA", "KT_NOIDUNG", "KT_THUONG" }))
            {


                // TODO: Add update logic here
                db.Entry(kt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_ktkl");

            }
            return View(kt);
        }

        // GET: Admin/KTKL/Delete/5
        [HttpGet]
        public ActionResult delete_ktkl(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHENTHUONGKYLUAT kt = db.KHENTHUONGKYLUATs.Find(id);
            if (kt == null)
            {
                return HttpNotFound();
            }
            return View(kt);
        }

        // POST: Admin/KTKL/Delete/5
        [HttpPost,ActionName("detele_ktkl")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            KHENTHUONGKYLUAT kt = db.KHENTHUONGKYLUATs.SingleOrDefault(n => n.KT_MA == id);

            db.KHENTHUONGKYLUATs.Remove(kt);
            db.SaveChanges();
            return RedirectToAction("list_ktkl");
        }
        
    }
}
