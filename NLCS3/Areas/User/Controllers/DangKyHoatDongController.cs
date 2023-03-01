using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.User.Controllers
{
    [Authorize(Roles = "vienchuc")]
    public class DangKyHoatDongController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: User/DangKyHoatDong
        public ActionResult hd_dadangky()
        {
            List<HOATDONG> hd1 = new List<HOATDONG>();
            List<CHITIETHOATDONG> ct1 = db.CHITIETHOATDONGs.Where(a => a.VC_MA == User.Identity.Name).ToList();
            foreach(var item in ct1)
            {
                foreach(var item1 in db.HOATDONGs.ToList())
                {
                    if(item.HD_MA== item1.HD_MA)
                    {
                        hd1.Add(item1);
                      
                    }
                }
            }           
            return View(hd1);            
        }

        public ActionResult hd_chuathamgia()
        {
            List<HOATDONG> hd = new List<HOATDONG>();
            List<HOATDONG> hd2 = new List<HOATDONG>();
            List<CHITIETHOATDONG> ct = db.CHITIETHOATDONGs.Where(a => a.VC_MA != User.Identity.Name).ToList();
            List<CHITIETHOATDONG> ct2 = db.CHITIETHOATDONGs.Where(a => a.VC_MA == User.Identity.Name).ToList();

            foreach(var a in ct2)
            {
                HOATDONG h = db.HOATDONGs.Where(x => x.HD_MA == a.HD_MA).FirstOrDefault();
                hd.Add(h);
            }
            hd2 = db.HOATDONGs.ToList();
            foreach(var a in hd2.ToList())
            {
                foreach(var b in ct2.ToList())
                {
                    if(a.HD_MA == b.HD_MA)
                    {
                        hd2.Remove(a);
                    }
                }
            }
            return View(hd2);
        }

        public ActionResult hd_dangky(int mahd)
        {
            CHITIETHOATDONG ct = new CHITIETHOATDONG();
            ct.VC_MA = User.Identity.Name;
            ct.HD_MA = mahd;
            db.CHITIETHOATDONGs.Add(ct);
            db.SaveChanges();
            return RedirectToAction("hd_dadangky");
        }
        public ActionResult chitiet(string search)
        {
            var hd = from s in db.HOATDONGs select s;
            if (!String.IsNullOrEmpty(search))
            {
                hd = hd.Where(s => s.HD_TEN.Contains(search));

            }
            return View(hd);
        }
        public FileResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Content/File"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}