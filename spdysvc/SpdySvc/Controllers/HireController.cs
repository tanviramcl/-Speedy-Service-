using SpdySvc.Models;
using SpdySvc.Models.SpdyMembershipUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using DBManager;
using SpdySvc.Utility;
using System.Text;
namespace SpdySvc.Controllers
{
    public class HireController : Controller
    {
        //
        // GET: /Hire/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HireView()
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
           List<Order> tnLst;
            FactoryPush fac = new FactoryPush();
            if (Convert.ToInt32(usr.RoleID) != 1)
            {
                tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(null, usr.UserID.ToString()));
            }
            else
            {
                tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(null,null));
            }

            ViewBag.TrnList = tnLst;
            ViewBag.Message = "Hire List ";
            return View();
        }

        public ActionResult HireDetails(String HID)
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            FactoryPush fac = new FactoryPush();
            List<OrderDetails> tnLst = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records(HID, "", "", ""));            
            ViewBag.TrnList = tnLst;
            ViewBag.Message = "Live Hire List ";
            return View();
        }

        public ActionResult EmailHireDetails(String OrderId) 
        {
            FactoryPush fac = new FactoryPush();
            List<OrderDetails> orderDlist = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records(OrderId,"","",""));
            ViewBag.OrderDetailsList = orderDlist;
            return View(); 
        }
        [HttpPost]
        public string AcceptData(String objItem)
        {
            OrderItem ordItem = new JavaScriptSerializer().Deserialize<OrderItem>(objItem);
            Double Total = 0;
           Total=ordItem.ToDate.Subtract(ordItem.FromDate).Days * ordItem.Quantity * ordItem.Value;


            return Total.ToString() ;
        }



        [HttpPost]
        public JsonResult AmmendHireDetails(String masterTransection,String detailsTransection)
        {
            FactoryPush fac = new FactoryPush();
            List<Order> tr = new JavaScriptSerializer().Deserialize<List<Order>>(masterTransection);    
        
            tr[0].CreatedDate = DateTime.Now;
         

            List<OrderItem> ordDetails = new JavaScriptSerializer().Deserialize<List<OrderItem>>(detailsTransection);
            //anis 15/04/2015 modified
            foreach (OrderItem oi in ordDetails) 
            {
                oi.FromDate = tr[0].OnHireDate;
                oi.ToDate = tr[0].OffHireDate;
            }
            EmailTemplate et= new EmailTemplate();
          
            fac.UpdateTransection(tr, ordDetails);
            ViewBag.Message = "Live Hire List ";

            // Sending Email to Requester
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();

            List<Order> orderlist = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records(tr[0].OrderId.ToString(), ""));
            EMAIL email = new EMAIL();
            foreach (Order orders in orderlist)
            {

              //  email.Message = et.OrderViewDetails(ordDetails, orders.Email);

                email.Message = et.OrderViewDetails(ordDetails, "tnvraiub@gmail.com");
            
            }

          
            email.Email ="tnvraiub@gmail.com";
            email.ContactNo = EmaiMessageContactNumber.ORDER_CONTACTNO;
            email.MessageSubject = EmaiMessageSubject.ORDER_MEESAGE_SUBJECT;
           
            email.Sender = "speedy@spp.cpom";
            email.SenderEmail = "skskjkx";
            fac.SendEmailNotification(email);
            //Send Same Mail To Admin
            if (SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Admin == true).Count > 0)
            {
                String adminEmail = SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Admin == true)[0].Email;
                email.Email = adminEmail;
            }
            //  fac.SendEmailNotification(email);

            //Audit Trail
            List<Audit_trail> adLst = new List<Audit_trail>();
            Audit_trail auDit = new Audit_trail();
            auDit.action = "Ammened Order no:" + tr[0].OrderId;
            auDit.date_time = DateTime.Now;
            auDit.script = "../Hire/AmmendHireDetails";
            auDit.user = usr.Email;
            adLst.Add(auDit);
            fac.SaveList<Audit_trail>(adLst);
            


            return Json("", JsonRequestBehavior.AllowGet);
        }

        //Anis 15/04/2015

       

        [HttpPost]
        public JsonResult ChangeHireStatus(string OrderIDListJsonString, string OpCode)    //OpCode 1 for Approve / 2 for Cancelled or rejected
        {

            String orderIds = "";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> ListOrderIds = serializer.Deserialize<List<int>>(OrderIDListJsonString);

            //
            EmailTemplate et = new EmailTemplate();
            FactoryPush fac = new FactoryPush();
            List<Order> tsLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records());
            List<Order> tlist = new List<Order>(); //list to edit
            //
          
           
            foreach(int orderid in ListOrderIds)
            {

                Random ra = new Random();
                StringBuilder largeRandomNumber = new StringBuilder();
                int randomNumber = ra.Next(100,10000);
                Order o = tsLst.Where(x => x.OrderId == Convert.ToInt32(orderid)).FirstOrDefault();
                orderIds += "  " + orderid + ",";
                if (OpCode == "1")
                {
                     o.OrderStatusID = 6; // approved
                }  
                else if(OpCode == "3")
                {
                    o.OrderStatusID = 1006;
                    List<OrderDetails> oCL = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records(randomNumber.ToString(), "", "", ""));
                    if (oCL.Count < 1)
                    {
                        o.CustomerOrderNumber = randomNumber.ToString();
                    }
                    
                }
                else
                    o.OrderStatusID = 1;// cancelled
                o.OrderStatus = null;
                o.FirstName = null;
                o.HireLimit = null;
                tlist.Add(o);
            }

            fac.EditList(tlist);

          
            List<Order> tnLst = fac.GetRecords<Order>(new BLSp().SP_Get_All_Order_Records());
            ViewBag.TrnList = tnLst;
            ViewBag.Message = "Live Hire List ";
            //order details
            //Sending Email to Requester

            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            
            EMAIL email = new EMAIL();
           // email.Email = usr.Email;
           
            email.ContactNo = EmaiMessageContactNumber.ORDER_CONTACTNO;
            email.MessageSubject = EmaiMessageSubject.ORDER_STATUS_MEESAGE_SUBJECT;
            email.Sender = "spedd@sdddd";
            email.SenderEmail = "sjkls@skjd";

            List<OrderDetails> orderDlist;
            orderDlist = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records("", "", "", ""));
            orderDlist = (from s in orderDlist
                     where ListOrderIds.Contains(s.OrderId)
                     select s).ToList();
            var ordLIst = orderDlist.GroupBy(itm => itm.Email);

            foreach (var obj in ordLIst) 
            {
              //  email.Email = obj.Key;
                email.Email = "tnvraiub@gmail.com";
                List<OrderDetails> ordLst = obj.ToList();
                email.Message = et.ApprovdOrderlist((OpCode.Equals("1") ? "Approved" : " Cancelled "),ordLst);
                fac.SendEmailNotification(email);
            }

       

          
            //Send Same Mail To Admin
            if (SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Admin == true).Count > 0)
            {
                String adminEmail = SingleTon.SingleTon.Instance().USER_LIST.FindAll(itm => itm.Admin == true)[0].Email;
                email.Email = adminEmail;
            }
          //  fac.SendEmailNotification(email);
            List<Audit_trail> adLst = new List<Audit_trail>();
            Audit_trail auDit = new Audit_trail();
            auDit.action = "" + (OpCode.Equals("1") ? "Approved" : "Cancel") + " Order(s) no:" + orderIds;
            auDit.date_time = DateTime.Now;
            auDit.script = "../Hire/ChangeHireStatus";
            auDit.user = usr.Email;
            adLst.Add(auDit);
            fac.SaveList<Audit_trail>(adLst);


            //end 

            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
        }



    

    }
}
