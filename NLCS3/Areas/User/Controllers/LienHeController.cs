using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.User.Controllers
{
    public class LienHeController : Controller
    {
        [Authorize(Roles = "vienchuc")]
        // GET: User/LienHe
        public ActionResult xemlienhe()
        {
            return View();
        }
    }
}