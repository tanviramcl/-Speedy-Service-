using SpdySvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpdySvc.Controllers
{
    public class ApproverController : Controller
    {
        //
        // GET: /Approver/

        public ActionResult Index()
        {
            //MembershipUser currentUser = Membership.GetUser();
            //string mess = "";
            //UserRecords usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.UserID == currentUser.UserName);
            //ViewBag.Message = " Approver Panel";
            
            return View();
        }

    }
}
