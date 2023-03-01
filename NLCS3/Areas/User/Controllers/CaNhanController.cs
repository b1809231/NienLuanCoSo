using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.User.Controllers
{
    [Authorize(Roles = "vienchuc")]
    public class CaNhanController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: User/CaNhan
        public ActionResult ThongTinCaNhan()
        {
            var vc = db.VIENCHUCs.Where(n => n.VC_MA == User.Identity.Name).FirstOrDefault();            
            return View(vc);
        }

        // GET: User/CaNhan/Details/5
        public ActionResult Sua_CaNhan()
        {
           
            var vc = db.VIENCHUCs.Where(n=>n.VC_MA == User.Identity.Name).FirstOrDefault();            
            return View(vc);
        }    
       
               

        // POST: User/CaNhan/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VIENCHUC vc)
        {

            var vienchuc = db.VIENCHUCs.Where(n => n.VC_MA == User.Identity.Name).FirstOrDefault();
            if (TryUpdateModel(vienchuc, "", new string[] { "VC_MA", "VC_ANH", "VC_HOTEN", "VC_NGAYSINH", "VC_GIOITINH", "VC_DIACHI", "VC_SDT", "VC_EMAIL", "VC_LUONG","VC_QUYENSD","VC_MATKHAU","BM_MA"}))
            {
                // TODO: Add update logic here
                db.Entry(vienchuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinCaNhan");
            }
            if (vienchuc.VC_MATKHAU == null)
            {
                return RedirectToAction("Sua_CaNhan", "CaNhan");
            }
            db.Entry(vienchuc).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("ThongTinCaNhan", "CaNhan");

        }

        // GET: User/CaNhan/Delete/5
       
    }
}
