using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NLCS3.Areas.Admin.Controllers
{
    [Authorize(Roles = "quanly")]
    public class VienChucController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/VienChuc
        
        public ActionResult list_vienchuc(string search)
        {
            var vc = from s in db.VIENCHUCs select s;
            if (!String.IsNullOrEmpty(search))
            {
                vc = vc.Where(s => s.VC_HOTEN.Contains(search));

            }
            return View(vc);
        }

        // GET: Admin/VienChuc/Details/5
        public ActionResult details_vienchuc(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var vc = db.VIENCHUCs.Find(id);
            if (vc == null)
            {
                return HttpNotFound();
            }
            return View(vc);
        }

        // GET: Admin/VienChuc/Create
        public ActionResult create_vienchuc()
        {

            List<BOMON> cate = db.BOMONs.ToList();
            SelectList cateList = new SelectList(cate, "BM_MA", "BM_TEN");
            ViewBag.CateList = cateList;
            ViewBag.Error = false;
            return View();
        }
        private bool TonTaiKhoaChinh(VIENCHUC vc)
        {
            int dem = db.VIENCHUCs.Count(a => a.VC_MA == vc.VC_MA);
            Console.WriteLine();
            return dem == 0;
        }

        // POST: Admin/VienChuc/Create
        [HttpPost, ActionName("create_vienchuc")]
        [ValidateInput(false)]
        public ActionResult Create(VIENCHUC vc, HttpPostedFileBase f)
        {
            ViewBag.Error = false;
            if (TonTaiKhoaChinh(vc))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(f.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Image/VienChuc/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ThongBao = "Hinh anh da co";
                        }
                        else
                        {
                            f.SaveAs(path);
                        }
                        vc.VC_ANH = f.FileName;
                        db.VIENCHUCs.Add(vc);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "ERROR");

                }
                var listVienChuc = from s in db.VIENCHUCs select s;
                return View("list_vienchuc", listVienChuc);
            }
            else
            {
                ViewBag.Error = true;
                return View("create_vienchuc", vc);
            }
        }



        // GET: Admin/VienChuc/Edit/5
        [HttpGet]
        public ActionResult edit_vienchuc(string id)
        {
            List<BOMON> cate = db.BOMONs.ToList();
            SelectList cateList = new SelectList(cate, "BM_MA", "BM_TEN");
            ViewBag.CateList = cateList;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIENCHUC vc = db.VIENCHUCs.Find(id);
            if (vc == null)
            {
                return HttpNotFound();
            }

            return View(vc);
        }
        
    // POST: Admin/VienChuc/Edit/5
    [HttpPost, ActionName("edit_vienchuc")]
    [ValidateInput(false)]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(string id)
        {

            var vc = db.VIENCHUCs.Find(id);
            if (TryUpdateModel(vc, "", new string[] { "VC_MA", "VC_ANH", "VC_HOTEN", "VC_NGAYSINH", "VC_GIOITINH", "VC_DIACHI", "VC_SDT", "VC_EMAIL", "LUONG", "VC_QUYENSD", "VC_MATKHAU", "BM_MA" }))
            {
                // TODO: Add update logic here
                db.Entry(vc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_vienchuc");
            }
            return View(vc);

        }

        // GET: Admin/VienChuc/Delete/5
        [HttpGet]
        public ActionResult delete_vienchuc(string id)
        {
            VIENCHUC vc = db.VIENCHUCs.SingleOrDefault(n => n.VC_MA == id);
            if (vc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vc);
        }

        // POST: Admin/VienChuc/Delete/5
        [HttpPost, ActionName("delete_vienchuc")]
        public ActionResult Delete(string id, FormCollection collection)
        {
            VIENCHUC vc = db.VIENCHUCs.SingleOrDefault(n => n.VC_MA == id);
            var path = Path.Combine(Server.MapPath("~/Content/Image/VienChuc"), vc.VC_ANH);

            if (vc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            db.VIENCHUCs.Remove(vc);
            db.SaveChanges();
            return RedirectToAction("list_vienchuc");
        }
    }
}
