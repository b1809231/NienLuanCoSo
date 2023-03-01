using NLCS3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NLCS3.Areas.Admin.Controllers
{
    [Authorize(Roles = "quanly")]
    public class SendMailController : Controller
    {
        // GET: Admin/SendMail
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(SendMail model)
        {
            using (MailMessage mail = new MailMessage(model.MAILGUI, model.MAILNHAN))
            {
                mail.Subject = model.CHUDE;
                mail.Body = model.NOIDUNG;
                if (model.FILE.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(model.FILE.FileName);
                    mail.Attachments.Add(new Attachment(model.FILE.InputStream, fileName));
                }
                mail.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(model.MAILGUI, model.PASS);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Email sent.";
                }
            }

            return View();
        }
    }
}
