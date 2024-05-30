using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using SpdySvc.Models;
using DBManager;
using SpdySvc.Service.PriceService;
namespace SpdySvc
{
    public class FactoryPush
    {
        /// <summary>
        /// Save Only One Record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="savLst"></param>
        /// <returns></returns>
        public String SaveList<T>(List<T> savLst) 
        {
            IFactory fac = new Factory();
            IRepository repo=  fac.repoSave<T>(savLst);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE+" "+repo.MESSAGE;

        }

     /// <summary>
        /// Save Two Records Together
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <typeparam name="U"></typeparam>
     /// <param name="FirstList"></param>
     /// <param name="SecondList"></param>
     /// <returns></returns>
        public String SaveList<T,U>(List<T> FirstList,List<U> SecondList)
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoSave<T,U>(FirstList,SecondList);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;

        }
        /// <summary>
        /// Save Three Records Together
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="FirstList"></param>
        /// <param name="SecondList"></param>
        /// <param name="ThirdList"></param>
        /// <returns></returns>
        public String SaveList<T, U, V>(List<T> FirstList, List<U> SecondList, List<V> ThirdList)
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoSave<T, U, V>(FirstList,SecondList, ThirdList);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;

        }

        /// <summary>
        /// Save Four Records Together
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="FirstList"></param>
        /// <param name="SecondList"></param>
        /// <param name="ThirdList"></param>
        /// <param name="ForthList"></param>
        /// <returns></returns>
        public String SaveList<T, U, V, W>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList)
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoSave<T, U, V, W>(FirstList, SecondList, ThirdList,ForthList);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;

        }

        /// <summary>
        /// Save Five Records Together
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <param name="FirstList"></param>
        /// <param name="SecondList"></param>
        /// <param name="ThirdList"></param>
        /// <param name="ForthList"></param>
        /// <param name="FifthList"></param>
        /// <returns></returns>
        public String SaveList<T, U, V, W, X>(List<T> FirstList, List<U> SecondList, List<V> ThirdList, List<W> ForthList, List<X> FifthList)
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoSave<T, U, V, W, X>(FirstList, SecondList, ThirdList,ForthList, FifthList);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;

        }

       
        /// <summary>
        /// Update a Previous Transection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="FisrtList"></param>
        /// <param name="SecondList"></param>
        /// <returns></returns>
        public String UpdateTransection<T,U>(List<T> FisrtList,List<U> SecondList)
        {
            IFactory fac = new Factory();
            fac.UpdateTransection<T, U>(FisrtList, SecondList);
            return fac.MESSAGE;
        }

        /// <summary>
        /// Edit Any Records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="edtLst"></param>
        /// <returns></returns>
        public String EditList<T>(List<T> edtLst) 
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoEdit<T>(edtLst);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;
        }

        /// <summary>
        /// Delete Any Records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="edtLst"></param>
        /// <returns></returns>
        public String DeleteLst<T>(List<T> edtLst)
        {
            IFactory fac = new Factory();
            IRepository repo = fac.repoDelete<T>(edtLst);
            fac.MESSAGE = repo.MESSAGE;
            fac.CODE = repo.CODE;
            return fac.CODE + " " + repo.MESSAGE;
        }

        /// <summary>
        /// Get Next Increment ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public String NextIncrementID<T>() 
        {
            IFactory fac = new Factory();
            IRepository repo = fac.NextIncrementID<T>();
            fac.NEXT_ID=repo.NEXT_ID;
            return fac.NEXT_ID;
        }

        /// <summary>
        /// Send Any Email Notification
        /// </summary>
        /// <param name="emailMdl"></param>
        /// <returns></returns>
        public String SendEmailNotification(EMAIL emailMdl) 
        {
            IFactory fac = new Factory();
            List<MailQueue> mailLst = new List<MailQueue>();
            mailLst.Add(new MailQueue { MailStatusID = "2", Sender = emailMdl.SenderEmail, BodyHtml = emailMdl.Message.Replace("'","\""), DateCreated = DateTime.Now, DateModified = DateTime.Now, Recipients = emailMdl.Email, Subject = emailMdl.MessageSubject });
            fac.SendEmail(emailMdl);
            SaveList<MailQueue>(mailLst);
            return "";
        }

        /// <summary>
        /// Get Rocords
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetRecords<T>() 
        {
            IFactory fac = new Factory();
            return fac.repoGetRecords<T>();
        }

        /// <summary>
        /// Get Rocords With Sql Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(String SqlQuery)
        {
            IFactory fac = new Factory();
            return fac.repoGetRecords<T>(SqlQuery);
        }

        /// <summary>
        /// Execute Command for Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbmCommand"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(IDBMCommand dbmCommand)
        {
            IFactory fac = new Factory();
            return fac.repoGetRecords<T>(dbmCommand);
        }

        /// <summary>
        /// Save a new User
        /// </summary>
        /// <param name="uList"></param>
        /// <returns></returns>
        public String SaveUser(List<User> uList)
        {
            IFactory fac = new Factory();
            fac.Newuser(uList);
            return fac.MESSAGE;
        }
      
        /// <summary>
        /// Execute Any Query ItSelf
        /// </summary>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public String EsecuteQuery(String SqlQuery) 
        {
            IFactory fac = new Factory();
            fac.ExecuteNonQuery(SqlQuery);
            return fac.MESSAGE;
        }


        /// <summary>
        /// Execute Single SoredProcedure
        /// </summary>
        /// <param name="dbcommand"></param>
        /// <returns></returns>
        public String ExecuteSP(IDBMCommand dbcommand)
        {
            IFactory fac = new Factory();
            fac.ExecuteCommand(dbcommand);
            return fac.MESSAGE;
        }


        //Added by Anis 22/04/2015
        public String AssignUserPermission(User aUser, Authorization.UserRoles aUserRole)
        {
            IFactory fac = new Factory();
            fac.AssignUserPermission(aUser, aUserRole);
            return fac.MESSAGE;
        }


        public PriceEnquiryDetailResponse RetriveRetDetails(List<PriceHelper_Product> proDlSt,bool isDelivery)
        {
            IFactory fac = new Factory();
            fac.GetRateDetails(proDlSt, false);
            return fac.PRICE_ENQUIRY_DETAIL_RESPONSE;
        }


        /// <summary>
        /// Create a new Order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public String PlaceNewOrder(List<Order> order, List<OrderItem> orderItem, List<OrderItemExtra> extra) 
        {
            IFactory fac = new Factory();
            fac.CreateNewOrder(order, orderItem, extra);
            return fac.MESSAGE;
        }
     
       
    }
}
