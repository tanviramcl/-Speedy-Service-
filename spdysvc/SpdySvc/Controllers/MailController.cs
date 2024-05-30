using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpdySvc.Models;
namespace SpdySvc.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailDetails(String IDN)
        {
            ViewBag.IDN = IDN;
            return View();
        }


    }
}
