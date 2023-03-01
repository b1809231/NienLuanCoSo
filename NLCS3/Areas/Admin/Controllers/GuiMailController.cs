using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.Admin.Controllers
{
    [Authorize(Roles = "quanly")]
    public class GuiMailController : Controller
    {
        // GET: Admin/GuiMail
        public ActionResult guimail()
        {
            return View();
        }
        [HttpPost ,ActionName("guimail")]
        public ActionResult SendEmail(string nhan, string chude, string noidung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guiEmail = new MailAddress("cttd35@gmail.com", "Summi");
                    var nhanEmail = new MailAddress(nhan, "Receiver");
                    var password = "Giadinhtoi1234";
                    var cd = chude;
                    var nd = noidung;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(guiEmail.Address, password)
                    };
                    using (var mess = new MailMessage(guiEmail, nhanEmail)
                    {
                        Subject = cd,
                        Body = nd,                       
                    })
                    {
                        smtp.Send(mess);
                    }
     
                    return View();

                }
                
            }
            catch (Exception)
            {
                ViewBag.Error = "Lỗi";
            }
            return View();
        }
    }
}