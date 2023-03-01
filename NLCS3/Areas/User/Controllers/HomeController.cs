using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.User.Controllers
{
    [Authorize(Roles = "vienchuc")]
    public class HomeController : Controller
    {
        // GET: User/Home
        public ActionResult HomePage()
        {
            return View();
        }
    }
}