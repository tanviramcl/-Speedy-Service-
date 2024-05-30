using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBManager;
using SpdySvc.Models;
using System.Web.Security;
using SpdySvc.Models.SpdyMembershipUser;
using SpdySvc.Utility;
using SpdySvc.Service.PriceService;
using ServiceReference;
using SendGrid;
using System.Net.Mail;
using System.Net;
namespace SpdySvc.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            EMAIL mal = new EMAIL();
            mal.Email = "das063020@gmail.com";
            mal.Message = "Hi";
            mal.MessageSubject = "Hi";
            mal.Sender = "speedyuk@speedy.com";
            mal.SenderEmail = "speedyuk@speedy.com";

            FactoryPush fac = new FactoryPush();
            fac.SendEmailNotification(mal);

            //var message = new SendGridMessage();


            //message.AddTo("das063020@gmail.com");

            ////set the sender
            //message.From = new MailAddress("speedyuk@dd.com");

            ////set the message body
            //message.Html = "<html><p>Hello</p><p>World</p></html>";

            ////set the message subject
            //message.Subject = "Hello World HTML Test";

            ////create an instance of the Web transport mechanism
            //var transportInstance = new Web(new NetworkCredential("speedy", "speedyuk@2015"));

            ////send the mail
            //transportInstance.DeliverAsync(message);

         //   List<PriceHelper_Product> pro = new List<PriceHelper_Product>();
         //   PriceHelper_Product pp = new PriceHelper_Product();
         //   pp.from_date = DateTime.Now;
         //   pp.product_Code = "01/0046-h";
         //   pp.qty = 2;
         //   pp.to_date = DateTime.Now.AddDays(3);
         //   pro.Add(pp);

           

         //   FactoryPush fac = new FactoryPush();
         ////  PriceEnquiryDetailResponse fac.RetriveRetDetails(pro, false);
         //   //List<User> ulst = new List<User>();
         //   //ulst.Add(new User { Token = Guid.NewGuid() });
         //   //fac.SaveList<User>(ulst);

         //   //List<OrderItem> orderlst = new List<OrderItem>();
         //   //orderlst = fac.GetRecords<OrderItem>();
         //   //EmailTemplate emailTemplate = new EmailTemplate();
         //   //emailTemplate.OrderViewDetails(orderlst);

         //   List<OrderDetails> OffHireOrderDetailsRecords = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_OffHire_Order_Details_Records(DateTime.Now));
         //   var groupEmailSender = OffHireOrderDetailsRecords.GroupBy(item => item.Email).ToArray();

          
            
         //   String mess = "";
         //   EmailTemplate emailTemplate = new EmailTemplate();
         //   FactoryPush factoryPush = new FactoryPush();
         //   string indexHTML = emailTemplate.OffhireOrderDetail(OffHireOrderDetailsRecords);
         //   ViewBag.rawHTML = indexHTML;

         //   foreach (var singleMail in groupEmailSender)
         //   {
         //       List<OrderDetails> ord = singleMail.ToList();
               
         //       EMAIL email = new EMAIL();
         //       email.Email = singleMail.FirstOrDefault().Email;
         //       email.Message = emailTemplate.OffhireOrderDetail(ord);
         //       email.Sender = singleMail.FirstOrDefault().Email;
         //       email.SenderEmail = singleMail.FirstOrDefault().Email;
         //       factoryPush.SendEmailNotification(email);
         //   }
            
         //   mess += "Sucess";
           

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
