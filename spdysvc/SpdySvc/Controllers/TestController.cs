using SpdySvc.Models;
using SpdySvc.Models.SpdyMembershipUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpdySvc.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserPermission()
        {
         SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
         List<PermissionDetails> lstpermissionDetails;
         FactoryPush fac = new FactoryPush();

            lstpermissionDetails = fac.GetRecords<PermissionDetails>(new BLSp().SP_GetUserPermissionDetails(usr.UserID.ToString()));
            ViewBag.permissLst = lstpermissionDetails;
            ViewBag.Message = "Permission Details List ";
            return View();
        }

    }
}
