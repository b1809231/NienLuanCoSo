using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NLCS3.Areas.Admin.Models
{
    public class SendMail
    {
        public string MAILNHAN { get; set; }
        public string CHUDE { get; set; }
        public string NOIDUNG { get; set; }
        public HttpPostedFileBase FILE { get; set; }
        public string MAILGUI{ get; set; }
        [DataType(DataType.Password)]
        public string PASS { get; set; }
    }
}