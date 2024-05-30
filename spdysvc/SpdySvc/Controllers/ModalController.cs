using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpdySvc.Models;
using SpdySvc.Provider;
using SpdySvc.Models.SpdyMembershipUser;
using System.Web.Security;
using SpdySvc.Utility;
namespace SpdySvc.Controllers
{
    public class ModalController : Controller
    {
        //
        // GET: /Modal/

        public ActionResult Index(String IDN)
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();

            String URL = URLs.ADMIN_HIRE_EDIT+"HID="+IDN;
            if (Convert.ToInt32(usr.RoleID) > 1)
                URL = URLs.REQUESTER_HIRE_EDIT + "HID=" + IDN; 
            ViewBag.URL = URL;
            return View();
        }
        public ActionResult Mail(String IDN)
        {                  
            ViewBag.URL ="../Mail/MailDetails?IDN="+IDN;            
            return View();
        }
        public ActionResult CommonModal(String IDN,String Type)
        {
            String URL = "";
            switch (Type)
            { 
                case "HL":
                    URL = "../Requestor/HireValue?IDN=" + IDN;
                    break;
                case "CNU":
                    URL = "../Account/Register";
                    break;
                case "VD":
                    URL = "../Users/PermissionDetails?UserId="+IDN;
                    break;
                case "EU":
                    URL = "../Users/EditUsers?UserId=" + IDN;
                    break;
            
            }
            ViewBag.URL = URL;
            return View();
        }
    }
}
