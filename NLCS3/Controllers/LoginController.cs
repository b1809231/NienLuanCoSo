using NLCS3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NLCS3.Controllers
{
    public class LoginController : Controller
    {
        NLCS_QLHDCDEntities12 db = new NLCS_QLHDCDEntities12();
        // GET: Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(VIENCHUC vc)
        {
            VIENCHUC test = db.VIENCHUCs.Where(A => A.VC_MA == vc.VC_MA && A.VC_MATKHAU == vc.VC_MATKHAU).FirstOrDefault();
            if(test == null)
            {
                ViewBag.Error="sai mat khau";
                return View();
            }
            else
            {
                if(test.VC_QUYENSD == "quanly")
                {
                    FormsAuthentication.SetAuthCookie(vc.VC_MA, false);
                    return RedirectToAction("Index", "Admin/Home");
                    
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(vc.VC_MA, false);
                    return RedirectToAction("HomePage", "User/Home");
                }

            }
           
        }
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }

    }
}