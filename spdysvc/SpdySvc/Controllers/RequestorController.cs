using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpdySvc.Utility;
using SpdySvc.Models;
using SpdySvc.Models.SpdyMembershipUser;
using System.Web.Security;
using System.Web.Script.Serialization;
using DBManager;
namespace SpdySvc.Controllers
{
    public class RequestorController : Controller
    {
        //
        // GET: /Requestor/

        public ActionResult Index()
        {
                        
            ViewBag.Message = " Requestor Panel ";
            return View();
        }

        public ActionResult Test(String IDN)
        {
            ViewBag.Message = " Data table test ";
            return View();
        }

        public ActionResult HireLimit() 
        {
            return View();
        }
        public ActionResult OffHire() 
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            List<Order> tnLst;
            FactoryPush fac = new FactoryPush();
            if (usr.SpdyAuthorization.AssignedUserRole!=Authorization.UserRoles.Admin)
            {
                if (usr.SpdyAuthorization.AssignedUserRole == Authorization.UserRoles.Requestor)
                { tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(null, usr.UserID.ToString())); }
                
            }
            else
            {
                tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(null, null));
            }

            tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(null, usr.UserID.ToString())).FindAll(itm => itm.OrderStatusID ==1006);
            ViewBag.TrnList = tnLst;
            ViewBag.Message = "Off Hire ";

            return View();
        }
        public ActionResult HireValue(String IDN)
        {
            List<User> usrList = SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.UserID.Equals(Convert.ToInt32(IDN)));
            ViewBag.USRLST = usrList;
            return View();
        }
        [HttpPost]
        public JsonResult HireValueChange(String hireMdl)
        {
            UserMdl uMdl = new JavaScriptSerializer().Deserialize<UserMdl>(hireMdl);
            List<UserMdl> uLst = new List<UserMdl>();
            uLst.Add(uMdl);
            FactoryPush fac = new FactoryPush();
            fac.EditList<UserMdl>(uLst);
           // SingleTon.SingleTon.Instance().GET_ALL_USER_RECORDS();
            User usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.UserID == uMdl.UserID);
            EMAIL email = new EMAIL();
            EmailTemplate emailtemplate= new EmailTemplate();
           // email.Email = usr.Email;
            email.Email = "tnvraiub@gmail.com";
            email.Message = emailtemplate.UpdateHireValue(uLst);
            email.MessageSubject = "HireValue";
            email.Sender = "Speedy@gmail.com";
            email.SenderEmail = "Speedy@gmail.com";
            fac.SendEmailNotification(email);
            SpdyMembershipUser user = (SpdyMembershipUser)Membership.GetUser();
            List<Audit_trail> aLst = new List<Audit_trail>();
            aLst.Add(new Audit_trail { action = "Successfully Update Hirevalue:" + user.Email, script = "../Account/Login", date_time = DateTime.Now, user = user.Email });
            fac.SaveList<Audit_trail>(aLst);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
