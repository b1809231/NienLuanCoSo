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
    public class BoMonController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/BoMon
        public ActionResult list_bomon()
        {
            var bm = from s in db.BOMONs select s;
            return View(bm);

        }

        // GET: Admin/BoMon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/BoMon/Create
        public ActionResult create_bomon()
        {
            List<KHOA> cate = db.KHOAs.ToList();
            SelectList cateList = new SelectList(cate, "K_MA", "K_TEN");
            ViewBag.CateList = cateList;
            return View();
        }


        // POST: Admin/BoMon/Create
        [HttpPost, ActionName("create_bomon")]
        [ValidateInput(false)]
        public ActionResult Create(BOMON bm)
        {
            if (ModelState.IsValid)
            {
                db.BOMONs.Add(bm);
                db.SaveChanges();
                return RedirectToAction("list_bomon");
            }

            return View(bm);
        }

        // GET: Admin/BoMon/Edit/5
        [HttpGet]
        public ActionResult edit_bomon(string id)
        {
            List<KHOA> cate = db.KHOAs.ToList();
            SelectList cateList = new SelectList(cate, "K_MA", "K_TEN");
            ViewBag.CateList = cateList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOMON bm = db.BOMONs.Find(id);
            if (bm == null)
            {
                return HttpNotFound();
            }
            return View(bm);
        }

        // POST: Admin/BoMon/Edit/5
        [HttpPost, ActionName("edit_bomon")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var bm = db.BOMONs.Find(id);
            if (TryUpdateModel(bm, "", new string[] { "BM_MA", "BM_TEN", "K_MA" }))
            {


                // TODO: Add update logic here
                db.Entry(bm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_bomon");

            }
            return View(bm);
        }

        // GET: Admin/BoMon/Delete/5
        [HttpGet]
        public ActionResult delete_bomon(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOMON bm = db.BOMONs.Find(id);
            if (bm == null)
            {
                return HttpNotFound();
            }
            return View(bm);
        }

        // POST: Admin/BoMon/Delete/5
        [HttpPost, ActionName("delete_bomon")]
        public ActionResult Delete(string id, FormCollection collection)
        {
            BOMON bm = db.BOMONs.SingleOrDefault(n => n.BM_MA == id);

            db.BOMONs.Remove(bm);
            db.SaveChanges();
            return RedirectToAction("list_bomon");
        }
    }
}
