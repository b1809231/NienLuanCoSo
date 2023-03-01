using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.Admin.Controllers
{
    [Authorize(Roles = "quanly")]
    public class HoatDongController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Admin/HoatDong
        public ActionResult list_hoatdong(string search)
        {
            var hd = from s in db.HOATDONGs select s;
            if (!String.IsNullOrEmpty(search))
            {
                hd = hd.Where(s => s.HD_TEN.Contains(search));

            }
            return View(hd);

            
        }

        // GET: Admin/HoatDong/Details/5
        public ActionResult details_hoatdong(int id)
        {
            return View();
        }

        // GET: Admin/HoatDong/Create
        public ActionResult create_hoatdong()
        {
            List<BOMON> cate = db.BOMONs.ToList();
            SelectList cateList = new SelectList(cate, "BM_MA", "BM_TEN");
            ViewBag.CateList = cateList;
            ViewBag.Error = false;
            return View();

        }
        public FileResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Content/File"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // POST: Admin/HoatDong/Create
        [HttpPost, ActionName("create_hoatdong")]
        [ValidateInput(false)]
        public ActionResult Create(HOATDONG hd, HttpPostedFileBase f )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (f != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/File"), Path.GetFileName(f.FileName));
                        f.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.ThongBao = "File đã có";
                    }
                    hd.HD_FILE = f.FileName;
                    db.HOATDONGs.Add(hd);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error");
                }
            }
            var listhd = from s in db.HOATDONGs select s;
            return View("list_hoatdong", listhd);

        }

        // GET: Admin/HoatDong/Edit/5
        public ActionResult edit_hoatdong(int? id)
        {
            List<BOMON> cate = db.BOMONs.ToList();
            SelectList cateList = new SelectList(cate, "BM_MA", "BM_TEN");
            ViewBag.CateList = cateList;
            ViewBag.Error = false;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOATDONG hd = db.HOATDONGs.Find(id);
            if (hd == null)
            {
                return HttpNotFound();
            }
            return View(hd);

        }

        // POST: Admin/HoatDong/Edit/5
        [HttpPost, ActionName("edit_hoatdong")]
        [ValidateInput(false)]

        public ActionResult Edit(int id, FormCollection collection)
        {
            var hd = db.HOATDONGs.Find(id);
            if (TryUpdateModel(hd, "", new string[] { "HD_MA", "BM_MA", "HD_MOTA", "HD_SOTIET", "HD_NGAYDIENRA", "HD_FILE" }))
            {
                // TODO: Add update logic here
                db.Entry(hd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_hoatdong");
            }
            return View(hd);

        }

        // GET: Admin/HoatDong/Delete/5
        [HttpGet]
        public ActionResult delete_hoatdong(int? id)
        {
            HOATDONG vc = db.HOATDONGs.SingleOrDefault(n => n.HD_MA == id);
            if (vc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vc);
        }

        // POST: Admin/HoatDong/Delete/5
        [HttpPost, ActionName("delete_hoatdong")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            HOATDONG hd = db.HOATDONGs.SingleOrDefault(n => n.HD_MA == id);
            var path = Path.Combine(Server.MapPath("~/Content/File"), hd.HD_FILE);

            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            db.HOATDONGs.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("list_hoatdong");
        }
    }
}
