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
    public class CTHDController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/CTHD
        public ActionResult list_cthd()
        {
            var ct = from s in db.CHITIETHOATDONGs select s;
            return View(ct);
        }

        // GET: Admin/CTHD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/CTHD/Create
        public ActionResult create_cthd()
        {
            List<HOATDONG> cate = db.HOATDONGs.ToList();
            SelectList cateList = new SelectList(cate, "HD_MA", "HD_TEN");
            ViewBag.CateList = cateList;

            List<VIENCHUC> cate1 = db.VIENCHUCs.ToList();
            SelectList cateList1 = new SelectList(cate, "VC_MA", "VC_HOTEN");
            ViewBag.CateList = cateList1;
            return View();
        }

        // POST: Admin/CTHD/Create
        [HttpPost, ActionName("create_cthd")]
        [ValidateInput(false)]
        public ActionResult Create(CHITIETHOATDONG ct)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHOATDONGs.Add(ct);
                db.SaveChanges();
                return RedirectToAction("list_cthd");
            }

            return View(ct);
        }

        // GET: Admin/CTHD/Edit/5
        public ActionResult edit_cthd(int? id)
        {

            List<HOATDONG> cate = db.HOATDONGs.ToList();
            SelectList cateList = new SelectList(cate, "HD_MA", "HD_TEN");
            ViewBag.CateList = cateList;

            List<VIENCHUC> cate1 = db.VIENCHUCs.ToList();
            SelectList cateList1 = new SelectList(cate, "VC_MA", "VC_HOTEN");
            ViewBag.CateList1 = cateList1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOATDONG ct = db.CHITIETHOATDONGs.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
           
        }

        // POST: Admin/CTHD/Edit/5
        [HttpPost, ActionName("edit_cthd")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var ct = db.CHITIETHOATDONGs.Find(id);
            if (TryUpdateModel(ct, "", new string[] { "HD_MA", "HD_GHICHU", "VC_MA" }))
            {


                // TODO: Add update logic here
                db.Entry(ct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_cthd");

            }
            return View(ct);
        }

        // GET: Admin/CTHD/Delete/5
        public ActionResult delete_cthd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOATDONG ct = db.CHITIETHOATDONGs.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Admin/CTHD/Delete/5
        [HttpPost, ActionName("delete_cthd")]
        public ActionResult Delete(int id, FormCollection collection)
        {
           CHITIETHOATDONG ct = db.CHITIETHOATDONGs.SingleOrDefault(n => n.HD_MA == id);

            db.CHITIETHOATDONGs.Remove(ct);
            db.SaveChanges();
            return RedirectToAction("list_cthd");
        }
    }
}
