using SpdySvc.Models;
using SpdySvc.Models.SpdyMembershipUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Configuration;
using DBManager;

using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.SmtpApi;
namespace SpdySvc.Utility
{
    public static class SessionVariable
    {

        public static String CART_SESSION = "CART_SESSION_VARIABLE";
        public static String CURRENT_CAT = "CURRENT_CAT_VARIABLE";
        public static String CURRENT_PRODUCT = "CURRENT_PRODUCT_VARIABLE";
        public static String CAT_PARENT_ID = "CAT_PARENT_ID_VARIABLE";
        public static String ROOT_CAT_ID = "ROOT_CAT_ID_VARIABLE";        
    }

    public static class URLs
    {
        public static String REQUESTER_HIRE_EDIT = "../Hire/HireDetails?";
        public static String ADMIN_HIRE_EDIT = "../Hire/HireDetails?";

    }

    public static class StoredProcedure
    {
        public static String SP_Get_All_Transection_Details_Redords = "SP_Get_All_Transection_Details_Redords"; //will be removed
        public static String SP_Get_All_Order_Records = "SP_Get_All_Order_Records";
        public static String SP_Get_All_Order_Details_Records = "SP_Get_All_Order_Details_Records";
        public static String SP_Get_Order_Records_byCustomerId = "SP_Get_Order_Records_byCustomerId";
        public static String SP_Get_OffHire_Order_Details_Records = "SP_Get_OffHire_Order_Details_Records";

        public static String SP_GetUserPermissionDetails = "SP_GetUserPermissionDetails";
        public static String SP_Get_Token_Records = "SP_Get_Token_Records";
        public static String SP_GetAllUserRecords = "SP_GetAllUserRecords";
        public static String SP_AssignUserRole = "SP_AssignUserRole";

        public static String SP_Get_All_Categorys = "SP_Get_All_Categorys";
        public static String SP_Get_Category_Products = "SP_Get_Category_Products";
        public static String SP_Get_ProductDetails = "SP_Get_ProductDetails";

    }
    public class EmaiMessageSubject
    {
        /// <summary>
        /// Ammend Order Message 
        /// </summary>
        public static String ORDER_MEESAGE_SUBJECT = " Order Changed ";

        /// <summary>
        ///  Order Status Changed Message 
        /// </summary>
        public static String ORDER_STATUS_MEESAGE_SUBJECT = "Order Approved";
    }

    public class EmaiMessageContactNumber
    {
        /// <summary>
        /// Ammend OrderContacNo
        /// </summary>
        public static String ORDER_CONTACTNO = "1111111111111";


    }
    public class CommonModal
    {
        /// <summary>
        /// HireLimit
        /// </summary>
        public static String HIRE_LIMIT = "HL";


    }

    public class Utility
    {
        /// <summary>
        /// Encrypt Password
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string EncryptPassword(string input, String tokens)
        {
            HashAlgorithm hashAlgorithm = new SHA1Managed();
            String[] splt = tokens.Split('-');
            string newToken = "";
            foreach (String str in splt)
            {
                if (newToken == "")
                    newToken += str.ToUpper();
                else
                    newToken += "-" + str.ToUpper();
            }
            Guid token = new Guid(newToken);
            var saltedPassword = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", token, input));
            var hash = hashAlgorithm.ComputeHash(saltedPassword);

            return HashToString(hash);

        }

        private static string HashToString(IEnumerable<byte> hash)
        {
            var result = new StringBuilder();

            foreach (byte b in hash)
            {
                result.Append(b.ToString("X2"));
            }

            return result.ToString();
        }

        public string SendEmailMessage(EMAIL email)
        {
            var message = new SendGridMessage();


            message.AddTo(email.Email);

            //set the sender
            message.From = new MailAddress("speedyuk@speedy.com");

            //set the message body
            message.Html = email.Message;

            //set the message subject
            message.Subject = email.MessageSubject;

            //create an instance of the Web transport mechanism
            var transportInstance = new Web(new NetworkCredential("speedy", "speedyuk@2015"));

            //send the mail
            transportInstance.DeliverAsync(message);
            return "OK";
        } 

        /// <summary>
        /// Valid User Password
        /// </summary>
        /// <param name="MD5pwd"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //public bool passWordValid(string MD5pwd, string pwd)
        //{
        //    //String pwds = EncryptPassword(pwd);
        //    return (EncryptPassword(pwd) == MD5pwd ? true : false);
        //}

    }


    public class EmailTemplate
    {
        public String OrderViewDetails(List<OrderItem> orderDetails,String Email)
        {
            String template = "";
            String Orders = "";

            Orders = "";
            FactoryPush fac = new FactoryPush();
            User usr = new User();

            foreach (OrderItem orderitem in orderDetails)
            {
                if (orderitem.HireBuy == null && orderitem.ContractNumber == null)
                {
                    orderitem.HireBuy = "N/A";
                    orderitem.ContractNumber = "N/A";

                }
                Orders += "<h1 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>OrderId :" + orderitem.OrderId + @"</h1>" +

                 //"<thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>" +

             " <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>" +
             "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                 " <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>HireBuy</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>ContractNumber</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderItemId</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>ProductCode</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>ProductName</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Quantity</th>" +
                 "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>ToDate</th>" +
                 "</tr>" +
                 "</thead>" +
                 "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.HireBuy + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.ContractNumber + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.OrderItemId + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.ProductCode + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.ProductName + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.Quantity + "</td>" +
                 "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderitem.ToDate + "</td>" +
                 "</tr>";
                usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.Email == Email);


                template = @"<!DOCTYPE html>
        <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
        <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
            <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
        </head>
        <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>

        <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;
        <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
            Hi,<br>
            <p> " + usr.FirstName + @"," + usr.LastName + @"</p><br>
            <p>Your order details  has been changed</p><br>
            <a target='_blank' href='" + ConfigurationManager.AppSettings["BaseAddress"].ToString() + @"/Hire/EmailHireDetails?OrderId=' " + orderitem.OrderId + @">View OrderDetails</a></br>  
        <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
           
        ";
                template = template + Orders +
            @"
      
            </tbody>
            </table>
        </div>

        </body>
        </html>
        ";

            }
            return template;

        }
        public String ResetPassword()
        {
            string template = "";


            template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
            <a target='_blank' href='" + ConfigurationManager.AppSettings["BaseAddress"].ToString() + @"/Account/ResetPassWord'>Change PassWord</a></br>  
            </div>

            </body>
            </html>
            ";

            return template;

        }

        public String UpdateHireValue(List<UserMdl> uLst)
        {
            string template = "";
            string strList = "";
            foreach (UserMdl user in uLst)
            {
                strList += "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + user.UserID + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + user.HireLimit + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + user.LimitPercentage + "</td>" +
                    "</tr>";
            }


            template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
                <h2 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>Your HireValue</h2>
                <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
                <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>UserID</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>HireLimit</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>LimitPercentage</th>
                 </tr>
                </thead>
                <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                    " + strList +
        @"
      
                </tbody>
                </table>
            </div>

            </body>
            </html>
            ";

            return template;



        }

        //Template for New User Sign UP
       // public String NewUserSignUp( string emailBody, string newUsersEmail)
        public String NewUserSignUp( List<User> newUsers)
        {
           // List<User> newUsers= (List<User>) TempData["NewUser"] ;
            string template = "";
            string strList = "";
            foreach (var  newuser in newUsers)
            {
                strList += "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + newuser.FirstName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + newuser.LastName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + newuser.Email + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + newuser.CreateDateTime + "</td>" +
                    "</tr>";
            }
           

            template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>New User Information</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
           
            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
                <h2 style='font-family:inherit;font-weight:500;line-height:1.1;color:inherit;margin-top:20px;margin-bottom:10px;font-size:20px'>Successfully Created  New User for SppedyService </br> </br> User Information</h2>
                <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
                <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>First Name</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Last Name</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Email Address</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Create Date</th>

                    </tr>
                </thead>
                <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                    " + strList +
                @"
      
                </tbody>
                </table>
            </div>

            </body>
            </html>
            ";

            return template;

        }

        public String OffhireOrderDetail(List<OrderDetails> OffhireOrders)
        {
           // List<User> newUsers= (List<User>) TempData["NewUser"] ;
            string template = "";
            string strList = "";
            foreach (var offhireorder in OffhireOrders)
            {
                strList += "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.OrderId + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.AccountName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.FirstName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.LastName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.OrderStatus + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.FromDate + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.ToDate + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.ProductCode + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + offhireorder.ProductName + "</td>" +
                    "</tr>";
            }
           

            template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>New User Information</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
           
            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
                <h2 style='font-family:inherit;font-weight:500;line-height:1.1;color:inherit;margin-top:20px;margin-bottom:10px;font-size:20px'>Off Hire Order products </br> </br> Products Information</h2>
                <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
                <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Order No</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Account No</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>First Name</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Last Name</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Order Status</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>From Date</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>To date</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Product Code</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Product Name</th>

                    </tr>
                </thead>
                <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                    " + strList +
                @"
      
                </tbody>
                </table>
            </div>

            </body>
            </html>
            ";

            return template;

        }
        public String OrderPlaced()
        {
            String template = @"
        <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
        <html xmlns='http://www.w3.org/1999/xhtml'>
        <head>
            <title>Your Speedy Order</title>
        </head>
       <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>


            <table style='margin-left:30px;' cellpadding='0' cellspacing='0' border='0' width='618'>
			    <tr>
				    <td style='padding-bottom:25px; padding-top:40px;'>

					    <div style='font-size: 28px; color:#c5173d; margin-bottom:15px; font-family: Arial, Helvetica, sans-serif;'>Your Speedy Order: <% orderId %></div>
					    <div style='font-weight:bold; font-size:14px; margin-bottom:20px; font-family: Arial, Helvetica, sans-serif;'>Thank you for placing an Order with Speedy. Your order has been received and is being processed. You order is an offer and if accepted by us will form a binding contract. You order will be accepted by us only when we send you an email notifying you that your order is ready for collection or that your order has been dispatched. Details of your order and our terms and conditions are set out below.</div>
                        <div style='font-weight:bold; font-size:14px; margin-bottom:20px; font-family: Arial, Helvetica, sans-serif;'>You will be able to view the status of this order in your <a href='<% order_history_link %>' style='text-decoration: none;'>order history</a></div>
                    
                        <table style='background:#f9f9f9; border-color: #d7d7d7; padding-bottom:18px; border-width:1px; border-style: solid; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
						    <tr>
							    <td>
                                    <table style='margin-left:18px; margin-top:18px;' cellpadding='0' cellspacing='0' width='580'>
									    <tr>
										    <td>
											    <div style='font-size: 24px; color:#c5173d; margin-bottom:15px; font-family: Arial, Helvetica, sans-serif;'>Your Items</div>
                                                            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
                                                                    <h2 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>Striped Rows</h2>
                                                                    <p style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;margin: 0 0 10px;'>The .table-striped class adds zebra-stripes to a table:</p>            
                                                                    <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
                                                                    <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
                                                                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Firstname</th>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Lastname</th>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Email</th>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Firstname</th>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Lastname</th>
                                                                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Email</th>

                                                                    </tr>
                                                                    </thead>
                                                                    <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                                                                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>John</td>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>Doe</td>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>john@example.com</td>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>John</td>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>Doe</td>
                                                                    <td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>john@example.com</td>     
                                                                    </tr>
      
                                                                    </tbody>
                                                                    </table>
                                                            </div>                                                                                                                                                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
				    <td style='padding-bottom:25px;'>
					    <table style='background:#f9f9f9; border-color: #d7d7d7; border-width:1px; border-style: solid; padding-bottom:18px; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
						    <tr>
							    <td>
								    <table style='margin-left:18px; margin-top:18px; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='580'>
									    <tr>
										    <td>
                                                <div style='font-size: 24px; color:#c5173d; margin-bottom:15px;'>Order Details</div>

                                                <% orderDetails %>
                                             </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
				    <td style='padding-bottom:25px;'>
					    <table style='background:#c52443; color:#ffffff; font-weight:bold; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
						    <tr>
							    <td>
								    <table style='margin-left:18px; margin-top:18px; margin-right:18px; margin-bottom:18px; text-align: center; color:#ffffff; font-weight:bold; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='580'>
									    <tr>
										    <td>
											    <div style='font-size:16px;'>Note:</div>
											    <div style='font-size:12px; line-height:18px;'>Pay as you go customers will be required to provide two forms of ID before equipment will be processed for hire (one photographic I.D as suitable proof of identity and one proof of address) which will need to be shown to the depot or delivery driver.</div>
                                            </td>
                                        </tr>
                                    </table>
                               </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
				    <td>
					    <table style='background:#f9f9f9; border-color: #d7d7d7; border-width:1px; border-style: solid; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
						    <tr>
							    <td style='padding-top:18px; padding-bottom:18px;'>
                                    <% orderCost %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style='padding-bottom: 25px'>
                        <table style='background: #FFFFFF; border-color: #D7D7D7; border-width: 1px; border-style: solid; border-top: none; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
                            <tr>
                                <td>
                                    <% orderTotal %>
                                </td>
                            </tr>                        
                        </table>
                    
                    </td>
                </tr>
                <tr>
				    <td style='padding-bottom:25px;'>
					    <div style='font-weight:bold; font-family: Arial, Helvetica, sans-serif; font-size:12px; text-align: center; margin-bottom:20px;'>You will be able to view the status of this order in your <a href='<% order_history_link %>' style='text-decoration: none;'>order history</a></div>
				    </td>
			    </tr>
                <tr>
				    <td style='padding-bottom:25px;'>
                        <div style='font-size:12px; margin-bottom:20px; font-family: Arial, Helvetica, sans-serif;'>If you are acting as a consumer (not for business purposes) then you have cancellation rights in relation to this order which are set out below</div>
                         <table style='background: #FFFFFF; border-width: 0; border: none; font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='616'>
                            <tr>
                                <td style='width:20px; vertical-align:top;'><span style='font-weight:bold'>1</span></td>
                                <td style='vertical-align:top; padding-bottom:15px;'>
                                    <div style='font-size:12px; font-weight:bold; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>How to cancel</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>1.1. Where you place an order through our Site for hire of Equipment, purchase of Products or Services you can cancel your order at any time before you receive a Contract Confirmation Email.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>1.2. If you wish to cancel your order please contact us on 0845 602 2957. Please have your Contract Confirmation Email to hand and be ready to quote your order number and to specify which Equipment, Product or Services you want to cancel Alternatively you may give us written notice of your cancellation. This can be done by emailing us at edesk@speedyservices.com or by writing to us at EDesk, Hire Direct, Newmarket House, 16 The Parks, Newton-Le-Willows WA12 0JQ. You must include your order number in the email or written notice, and clearly specify which Equipment, Products or Services you wish to cancel.</div>
                                </td>
                            </tr>
                             <tr>
                                <td style='width:20px; vertical-align:top;'><span style='font-weight:bold'>2</span></td>
                                <td style='vertical-align:top; padding-bottom:15px;'>
                                    <div style='font-size:12px; font-weight:bold; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'> Your right to cancel if you are a consumer (not for business purposes)</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.1. Following receipt of the Products, you can cancel your order by notifying us (as set out in below) within 7 working days of the date on which the Products were delivered. This means that if you change your mind about the Products, or for any other reason you decide you do not want to keep the Products, you can notify us of your decision to cancel your order, and then return the Products to us for a refund. If your order comprises more than one item you can cancel and return any of the items individually or in combination.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.2. Subject to 2.1 above, where you hire Equipment or purchase Services if we have delivered the Equipment or you have taken possession of Equipment or we have commenced providing the Services within the cancellation period provided for in the Consumer Protection (Distance Selling) Regulations 2000 then you will no longer have a right to cancel the hire of the Equipment or provision of the Services.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.3. Where you cancel your order after the Products have been delivered or collected you agree to return the Products to us by following the procedure set out below.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.4. You agree to return the Products in full, in a resalable condition and securely packaged (properly repacked in their original packaging if they were removed from it) and unused. You are responsible for all costs of returning the Products subject to the exceptions set out below.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.5. Nothing within these conditions affects your rights under law, and in particular you will always be entitled to return products to us for a full refund if they were misdescribed, or are not of satisfactory quality or reasonably fit for their purpose and we cannot remedy the fault to your reasonable satisfaction. Only in such circumstances and/or if we provide you with substitute products which you do not want will we refund your reasonable costs to return the products to us.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>2.6. If the Products are damaged whilst in your care (including damage caused in assembling or disassembling the Products), if you fail to return all parts of the Products or otherwise if your failure to take reasonable care of the products means that we reasonably believe that resale will not be possible at all, you agree that we may withhold payment of all or part of the refund in respect of the Products as compensation for our loss on these Products. You should make all reasonable attempts to examine the Products before removing the protective covering, and agree that removing the protective covering will constitute a failure to take reasonable care of the Products.</div>                               
                                </td>
                            </tr>
                             <tr>
                                <td style='width:20px; vertical-align:top;'><span style='font-weight:bold'>3</span></td> 
                                <td style='vertical-align:top;'>
                                    <div style='font-size:12px; font-weight:bold; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>Refunding your money</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>3.1. We will make all refunds to the card used for payment or to your credit account as applicable.</div>
                                    <div style='font-size:12px; margin-bottom:8px; font-family: Arial, Helvetica, sans-serif;'>3.2. The refund will include the Charges you have paid to us for the cancelled Equipment, Services or returned Products (provided the provisions set out above are complied with)</div>
                                </td>
                            </tr>                                                 
                        </table>               
                    </td>
                </tr>

            </table>       

		    <img src='<% emailDomain %>/imgs/email/footer.png' alt='Footer' style='display:block;' width='680' height='147' />
		    <div style='width:680px; height:74px; background:#c52443;'>
			    <table style='font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='680' >
				    <tr>
					    <td style='text-align: right; background:#c52443; padding-right:50px;' width='680' height='74'>
						    <img src='<% emailDomain %>/imgs/email/footer-find-us-text.png' alt='Find Us Online' />
						    <a href='#'><img style='border:none;' src='<% emailDomain %>/imgs/email/icon-twitter.png' alt='Twitter' /></a>
						    <a href='#'><img style='border:none;' src='<% emailDomain %>/imgs/email/icon-facebook.png' alt='Facebook' /></a>
						    <a href='#'><img style='border:none;' src='<% emailDomain %>/imgs/email/icon-linkedin.png' alt='Linkedin' /></a>
						    <a href='#'><img style='border:none;' src='<% emailDomain %>/imgs/email/icon-youtube.png' alt='Youtube' /></a>
						    <a href='#'><img style='border:none;' src='<% emailDomain %>/imgs/email/icon-flickr.png' alt='Flickr' /></a>
					    </td>
				    </tr>
			    </table>
		    </div>
		    <div style='width:680px; height:50px; background:#464646;'>
			    <table style='font-family: Arial, Helvetica, sans-serif;' cellpadding='0' cellspacing='0' width='680' bgcolor='464646'>
				    <tr>
					    <td style='text-align: center; padding-top:20px; padding-bottom:20px; color:#FFFFFF; font-weight:bold; font-size:12px;'>Copyright &copy; 2013 Speedy Hire Plc. All rights reserved</td>
				    </tr>
			    </table>
		    </div>
	    </body>
    </html>";
            return "";
        }
        public String ApprovedOrder(String OrderStatus, List<int> orderId)
        {

           
            String template = "";
            String Orders = "";
            foreach (int i in orderId)
            {
                Orders = "";
                FactoryPush fac = new FactoryPush();

                List<OrderDetails> orderDlist = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records(i.ToString(), "", "", ""));
                foreach (OrderDetails orderdetails in orderDlist)
                {
                    Orders += "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OrderId + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OnHireDate + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OffHireDate + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.FirstName + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.TotalHireCost + "</td>" +
                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OrderStatus + "</td>" +
                    "</tr>";


                template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
                <h1 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>OrderId" + orderdetails.OrderId + @"</h1>
                <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
                <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderId</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OnHireDate</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OffHireDate</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Requestator</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>TotalHireCost</th>
                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderStatus</th>
                 </tr>
                </thead>";
               
             

            template = @"
                <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                    " + Orders +
        @"
      
                </tbody>
                </table>
            </div>

            </body>
            </html>
            ";
           }

            }

            return template;
        }

        public String ApprovdOrderlist(String OrderStatus,List<OrderDetails> orderDetails)
        {


            String template = "";
            String Orders = "";
           
            Orders = "";
            FactoryPush fac = new FactoryPush();
            User usr = new User();

            foreach (OrderDetails orderdetails in orderDetails)
            {
                Orders += "<h1 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>OrderId :" + orderdetails.OrderId + @"</h1>" +

                //"<thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>" +
                
            " <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>"+
            "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>"+
                " <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OnHireDate</th>" +
                "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OffHireDate</th>" +
                "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Requestator</th>" +
                "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>TotalHireCost</th>" +
                "<th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderStatus</th>" +
                "</tr>"+
                "</thead>"+
                "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
                "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OnHireDate + "</td>" +
                "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OffHireDate + "</td>" +
                "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.FirstName + "</td>" +
                "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.TotalHireCost + "</td>" +
                "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OrderStatus + "</td>" +
                "</tr>";
                usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.Email == orderdetails.Email);

            }
                template = @"<!DOCTYPE html>
        <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
        <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
            <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
            <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
        </head>
        <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>

        <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;
        <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
        Hi,<br>
        <p>" + usr.FirstName + @"," + usr.LastName + @"</p><br>
        <p>Your orders from Speedy service hasbeen  " + OrderStatus + @"</p><br>
        <p>Your order details</p>
        <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
           
        ";
                template=template+Orders +
            @"
      
            </tbody>
            </table>
        </div>

        </body>
        </html>
        ";
                
            return template;
        }

        public String CreateUser(List<User> userList)
        {


            String template = "";
            String Users = "";

            Users = "";
            FactoryPush fac = new FactoryPush();
            User usr = new User();

            foreach (User user in userList)
            {
                //Users += "<p style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'> :" + user.UserID + @"</p>" +
                //                    Users += "<p style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>OrderId :" + user.Password + @"</p>" +

                ////"<thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>" +


                //usr = SingleTon.SingleTon.Instance().USER_LIST.Find(itm => itm.Email == user.Email);
            }
        
            template = @"<!DOCTYPE html>
            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
            </head>
            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>

            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;
            <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
             Hi,<br>
             <p> " + usr.FirstName + @"," + usr.LastName + @"</p><br>
            <p>Your Account has been recently created into  Speedy service</p><br>
            <p>Please Login With:</p>
            <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
           
            ";
            template = template + Users +
        @"
      
                </tbody>
                </table>
            </div>

            </body>
            </html>
            ";


            return template;
        }






//        public String ApprovedOrder(String OrderStatus, int orderId)
//        {


//            String template = "";
//            String Orders = "";
          
//                Orders = "";
//                FactoryPush fac = new FactoryPush();

//                List<OrderDetails> orderDlist = fac.GetRecords<OrderDetails>(new BLSp().SP_Get_All_Order_Details_Records(orderId, "", "", ""));
//                foreach (OrderDetails orderdetails in orderDlist)
//                {
//                    Orders += "<tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OrderId + "</td>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OnHireDate + "</td>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OffHireDate + "</td>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.FirstName + "</td>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.TotalHireCost + "</td>" +
//                    "<td style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;line-height: 1.42857143;vertical-align: top;border-top: 1px solid #ddd;background-color: #fff!important;'>" + orderdetails.OrderStatus + "</td>" +
//                    "</tr>";


//                    template = @"<!DOCTYPE html>
//            <html lang='en' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;font-family: sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-size: 10px;-webkit-tap-highlight-color: rgba(0,0,0,0);'>
//            <head style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
//                <title style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>Bootstrap Example</title>
//                <meta charset='utf-8' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
//                <meta name='viewport' content='width=device-width, initial-scale=1' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
//                <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
//                <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
//                <script src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'></script>
//            </head>
//            <body style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;margin: 0;font-family: &quot;Helvetica Neue&quot;,Helvetica,Arial,sans-serif;font-size: 14px;line-height: 1.42857143;color: #333;background-color: #fff;'>
//            <div class='container' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;'>
//                <h1 style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;orphans: 3;widows: 3;page-break-after: avoid;font-family: inherit;font-weight: 500;line-height: 1.1;color: inherit;margin-top: 20px;margin-bottom: 10px;font-size: 30px;'>OrderId" + orderdetails.OrderId + @"</h1>
//                <table class='table table-striped' style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;border-spacing: 0;border-collapse: collapse!important;background-color: transparent;width: 100%;max-width: 100%;margin-bottom: 20px;'>
//                <thead style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;display: table-header-group;'>
//                    <tr style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;page-break-inside: avoid;'>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderId</th>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OnHireDate</th>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OffHireDate</th>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>Requestator</th>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>TotalHireCost</th>
//                    <th style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;padding: 8px;text-align: left;line-height: 1.42857143;vertical-align: bottom;border-top: 1px solid #ddd;border-bottom: 2px solid #ddd;background-color: #fff!important;'>OrderStatus</th>
//                 </tr>
//                </thead>";



//                    template = @"
//                <tbody style='-webkit-box-sizing: border-box;-moz-box-sizing: border-box;box-sizing: border-box;'>
//                    " + Orders +
//                @"
//      
//                </tbody>
//                </table>
//            </div>
//
//            </body>
//            </html>
//            ";
//                }

        
//            return template;
//        }
    }


}