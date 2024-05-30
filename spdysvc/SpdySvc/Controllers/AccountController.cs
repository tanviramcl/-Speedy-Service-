using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SpdySvc.Models;
using System.Web.Script.Serialization;
using SpdySvc.Provider;
using SpdySvc.Models.SpdyMembershipUser;
using DBManager;
using System.Configuration;
using SpdySvc.Utility;
using SpdySvc.Service.PriceService;
namespace SpdySvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Session["URL"] == null)
            {
                Session["URL"] = ViewBag.ReturnUrl = returnUrl;
            }
           
            ViewBag.Title = "Login";
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(String loginMdl, String returnUrl)
        {
            Users mdl = new JavaScriptSerializer().Deserialize<Users>(loginMdl);
            Boolean bol=new SqlServerMemberShipProvider().ValidateUser(mdl.UserID, mdl.Password);
            String msg="SUCCESS";
           
            if(bol)
            {
                msg += "-" + Session["URL"].ToString();
                Session["URL"] = null;
                SingleTon.SingleTon.Instance().GetUserPermission(mdl.UserID);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
       

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            new SqlServerMemberShipProvider().LogOff();
            //String cid = TempData["CateGoryID"].ToString();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Registration(String resistationmdl)
        {
            List<User> newUsers = new List<Models.User>();
            List<User> mdl = new JavaScriptSerializer().Deserialize<List<User>>(resistationmdl);
            FactoryPush callFactory = new FactoryPush();
            foreach (var item in mdl)
            {
                item.CreateDateTime = DateTime.Now;
                newUsers.Add(item);
            }
            String msg = callFactory.SaveUser(newUsers);
           //email
            String mess = "";
           // var emailMessage = "New User ( ";
           // var emailAddress = "";
            EmailTemplate emailTemplate = new EmailTemplate();
            FactoryPush factoryPush = new FactoryPush();
            EMAIL email = new EMAIL();
            email.Email = "lincolnhalder54@gmail.com";
            
           // email.Message = emailMessage+") added";
           //TempData["NewUser"] = mdl;
           // email.Message = emailTemplate.NewUserSignUp(email.Message, emailAddress);
            email.Message = emailTemplate.NewUserSignUp(newUsers);
            email.Sender = "lincolnhalder54@gmail.com";
            email.SenderEmail = "lincolnhalder54@gmail.com";
            factoryPush.SendEmailNotification(email);
            mess += "Sucess";
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// This method is called by a remote validation 
        /// if choosing user name already exist then 
        /// sending an error
        /// </summary>
        /// <param name="UserName">the passing user name that was chossed by client/user</param>
        /// <returns></returns> 
       [AllowAnonymous]
        public JsonResult UserOk(String UserName)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

       /// <summary>
       /// This method is called by a remote validation 
       /// if choosing user name already exist then 
       /// sending an error
       /// </summary>
       /// <param name="UserName">the passing user name that was chossed by client/user</param>
       /// <returns></returns> 
       [AllowAnonymous]
       public JsonResult EmailOk(String Email)
       {
           List<User> uLs = SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm=>itm.Email==Email);
           return Json(uLs.Count == 0, JsonRequestBehavior.AllowGet);
       }

        //
        // POST: /Account/Register
       public JsonResult GetUser()
       {
           

           SpdyMembershipUser currentUser =(SpdyMembershipUser)Membership.GetUser();
        
           User usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.UserID ==Convert.ToInt32(currentUser.UserName));
           //return Json("Hi "+usr.FirstName+" "+usr.LastName, JsonRequestBehavior.AllowGet);
           return Json(usr, JsonRequestBehavior.AllowGet);
       }

       public JsonResult GetRole()
       {
           SpdyMembershipUser currentUser = (SpdyMembershipUser)Membership.GetUser();
        
           User usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.UserID ==Convert.ToInt32(currentUser.UserName));
           return Json(currentUser.RoleID, JsonRequestBehavior.AllowGet);
       }

       public ActionResult ChangeAccount()
       {
           return View();
       }

       [AllowAnonymous]
       public ActionResult ResetPassWord(String Token)
       {
           String TokenID = Token;
           FactoryPush fac = new FactoryPush();
           List<Token> tcLst = fac.GetRecords<Token>(new BLSp().SP_Get_Token_Records(TokenID));

           ViewBag.TcLst = tcLst;
      
           return View();
       }

       public ActionResult AccountUpdate()
       {
           return View();
       }

       public ActionResult NewAcc()
       {
           return View();
       }
       [AllowAnonymous]
       public ActionResult ForgottenPassword() 
       {

           return View();
       }
       [AllowAnonymous]
       public JsonResult EmailCheck(String Email)
      {
           List<User> uLs = SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Email == Email);
           return Json(uLs.Count>0, JsonRequestBehavior.AllowGet);
           
       }

       [HttpPost]
       [AllowAnonymous]
       public JsonResult ForgetPassword(String forgetPasswordFormmdl)
       {
           String mess = "";
           ForgetPassWord forgetPass = new JavaScriptSerializer().Deserialize<ForgetPassWord>(forgetPasswordFormmdl);
           EmailTemplate et= new EmailTemplate();
           FactoryPush fac = new FactoryPush();
           Token token = new JavaScriptSerializer().Deserialize<Token>(forgetPasswordFormmdl);
           List<Token> tokenlist = new List<Token>();
           tokenlist.Add(token);
           String toc1 = Guid.NewGuid().ToString();
           String toc2 = toc1.Replace("-","");
           String Message = "";
           foreach (Token tokens in tokenlist) {

               tokens.TokenId = toc2;
               tokens.UserId = forgetPass.Email;
               tokens.Link = "" + ConfigurationManager.AppSettings["BaseAddress"].ToString() + @"/Account/ResetPassWord?Token=" + toc2;
               Message = tokens.Link;
           } 
           fac.SaveList<Token>(tokenlist);

           User usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.Email == forgetPass.Email);

           EMAIL email = new EMAIL();
           email.Email = usr.Email;
           email.Message = Message;
           email.MessageSubject = "ResetPassword";
           email.Sender = "Speedy@gmail.com";
           email.SenderEmail = "Speedy@gmail.com";
           fac.SendEmailNotification(email);
           mess += "Sucess";
           return Json(mess, JsonRequestBehavior.AllowGet);

       }

       [HttpPost]
       [AllowAnonymous]
       public JsonResult ChangePassword(String changepasswordmdl)
       {
           string mess = "";
           User mdl = new JavaScriptSerializer().Deserialize<User>(changepasswordmdl);
           List<User> uLst = new List<User>();
           uLst.Add(mdl);
           foreach (User user in uLst)
           {
               user.Token = Guid.NewGuid();
               user.Email = mdl.Email;
               user.Password = new Utility.Utility().EncryptPassword(user.Password, user.Token.ToString());
           }
           FactoryPush callFactory = new FactoryPush();
           callFactory.EditList(uLst);

           mess += "Sucess";
           return Json(mess, JsonRequestBehavior.AllowGet);

       }
       
    }
}

