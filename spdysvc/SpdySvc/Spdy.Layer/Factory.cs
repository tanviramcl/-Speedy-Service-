using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using DBManager;
using SpdySvc.Models;
using SpdySvc.Service.PriceService;
using ServiceReference;
namespace SpdySvc
{
    public class Factory : IFactory
    {
        public string MESSAGE { get; set; }

        public string CODE { get; set; }

        public string NEXT_ID { get; set; }

        public IRepository repoSave<T>(List<T> svLst)
        {
            IRepository repo = new Repository();
            repo.Save<T>(svLst);
            return repo;
        }

        public IRepository repoEdit<T>(List<T> edtlst)
        {
            IRepository repo = new Repository();
            repo.Edit<T>(edtlst);
            return repo;
        }

        public IRepository NextIncrementID<T>()
        {
            IRepository repo = new Repository();
            repo.NextIncrementID<T>();
            return repo;
        }

        public IRepository repoDelete<T>(List<T> delLst)
        {
            IRepository repo = new Repository();
            repo.Delete<T>(delLst);
            return repo;
        }


        public List<T> repoGetRecords<T>()
        {
            IRepository repo = new Repository();
            return repo.RetriveRecords<T>();            
        }

        public List<T> repoGetRecords<T>(string SqlQuery)
        {
            IRepository repo = new Repository();
            return repo.RetriveRecords<T>(SqlQuery);
        }

        public List<Users> SPItemUser(string UserID)
        {
            using (var obj = new BLSp())
            {
             //  return obj.SP_ItemUsr(UserID);
            }
            return new List<Users>();
        }



        public List<Users> SQlItemUser(string UserID)
        {
            throw new NotImplementedException();
        }


        public IRepository repoSave<T, U>(List<T> FirstLst, List<U> SecondList)
        {
            IRepository repo = new Repository();
            repo.Save<T, U>(FirstLst, SecondList);
            return repo;
        }

        public IRepository repoSave<T, U, V>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList)
        {
            IRepository repo = new Repository();
            repo.Save<T, U, V>(FirstLst, SecondList, ThirdList);
            return repo;
        }

        public IRepository repoSave<T, U, V, W>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList, List<W> ForthList)
        {
            IRepository repo = new Repository();
            repo.Save<T, U, V, W>(FirstLst, SecondList, ThirdList, ForthList);
            return repo;
        }

        public IRepository repoSave<T, U, V, W, X>(List<T> FirstLst, List<U> SecondList, List<V> ThirdList, List<W> ForthList, List<X> FifthList)
        {
            IRepository repo = new Repository();
            repo.Save<T, U, V, W, X>(FirstLst, SecondList, ThirdList, ForthList, FifthList);
            return repo;
        }

        public List<T> repoGetRecords<T>(IDBMCommand dbmCommand)
        {
            IRepository repo = new Repository();
            return repo.RetriveRecords<T>(dbmCommand);
        }

        public void Newuser(List<User> uList)
        {
            List<UserRole> rLst = new List<UserRole>();
            foreach (User usr in uList) 
            {
                Guid token = Guid.NewGuid();
                usr.Password = new Utility.Utility().EncryptPassword(usr.Password, token.ToString());
                usr.Token = token;

                //rLst.Add(new UserRole { RoleID = SingleTon.SingleTon.Instance().ROLE_LIST.Find(itm => itm.Priority.Equals("3")).IDN, userID = usr.UserID });
            }
            FactoryPush fac = new FactoryPush();

            List<Authorization.Tbl_UserRole> lstUserRole=new List<Authorization.Tbl_UserRole>();
      

            Authorization.Tbl_UserRole aTbl_UserRole = new Authorization.Tbl_UserRole();
            aTbl_UserRole.User_ID = fac.NextIncrementID<User>();
            aTbl_UserRole.Role_ID=uList[0].Role_ID.ToString();

            lstUserRole.Add(aTbl_UserRole);

            uList[0].Role_ID = null;

            var dbContext = new DBMContext();
            using (dbContext) 
            {
                dbContext.SaveRecords<User>(uList);
                dbContext.SaveRecords<UserRole>(rLst);
                dbContext.SaveRecords<Authorization.Tbl_UserRole>(lstUserRole);
            }
            MESSAGE = dbContext.Message;

        }


        //Added by Anis
       public void AssignUserPermission(User aUser,Authorization.UserRoles aUserRole)
        {
            List<Authorization.Tbl_UserRole> lstUserRole;
             List<Authorization.Tbl_RolePermission> lstRolePermission ;

            if (aUserRole == Authorization.UserRoles.Admin)
            {
                lstUserRole = new List<Authorization.Tbl_UserRole>();
                lstUserRole[0].User_ID = aUser.UserID.ToString();
                lstUserRole[0].Role_ID =((int) Authorization.UserRoles.Admin).ToString();

                lstRolePermission= new List<Authorization.Tbl_RolePermission>();
                lstRolePermission[0].Permission_ID = "16";//Permission ID for admin ; permission is a static table
                lstRolePermission[0].Role_ID = ((int)Authorization.UserRoles.Admin).ToString();

                
            }

            else if (aUserRole == Authorization.UserRoles.Approver)
            {
                lstUserRole = new List<Authorization.Tbl_UserRole>();
                lstUserRole[0].User_ID = aUser.UserID.ToString();
                lstUserRole[0].Role_ID = ((int)Authorization.UserRoles.Approver).ToString();

                lstRolePermission = new List<Authorization.Tbl_RolePermission>();
                lstRolePermission[0].Permission_ID = "15";//Permission ID for approver
                lstRolePermission[0].Role_ID = ((int)Authorization.UserRoles.Approver).ToString();
            }
            else
            {
                lstUserRole = new List<Authorization.Tbl_UserRole>();
                lstUserRole[0].User_ID = aUser.UserID.ToString();
                lstUserRole[0].Role_ID = ((int)Authorization.UserRoles.Requestor).ToString();

                lstRolePermission = new List<Authorization.Tbl_RolePermission>();
                lstRolePermission[0].Permission_ID = "2";//Permission ID for Requestor
                lstRolePermission[0].Role_ID = ((int)Authorization.UserRoles.Requestor).ToString();
              
            }

            var dbContext = new DBMContext();
            using (dbContext)
            {
                dbContext.SaveRecords<Authorization.Tbl_UserRole>(lstUserRole);
                dbContext.SaveRecords<Authorization.Tbl_RolePermission>(lstRolePermission);
            }
            MESSAGE = dbContext.Message;
        
        }





        public void SendEmail(EMAIL emailMdl)
        {
            //var context = new DBMContext();
            //using (context) 
            //{
            //    context.SendEmail(emailMdl);
            //}
            new Utility.Utility().SendEmailMessage(emailMdl);
            MESSAGE = "SUCCESS";
        }




        public void ExecuteNonQuery(string Query)
        {
            var context = new DBMContext();
            using (context)
            {
                context.RunLineQuery(Query);
            }
            MESSAGE = context.Message;
        }


        public void UpdateTransection<T, U>(List<T> FirstList, List<U> SecondList)
        {
            var context = new DBMContext();
            using(context)
            {              

                context.UpdateRecords<T>(FirstList);
                context.DeleteRecords<U>(SecondList);
                context.SaveRecords<U>(SecondList);
            }
            MESSAGE = context.Message;
        }


        public void CreateNewTransection<T, U>(List<T> FirstList, List<U> SecondList)
        {
            var context = new DBMContext();
            using (context)
            {
                context.SaveRecords<T>(FirstList);
                context.SaveRecords<U>(SecondList);
            }
            MESSAGE = context.Message;
        }



        public void ExecuteCommand(IDBMCommand command)
        {
            var context = new DBMContext();
            using ( context )
            {
                context.ExecuteSpCommand(command);
            }
            MESSAGE = context.Message;
        }


        public PriceEnquiryDetailResponse PRICE_ENQUIRY_DETAIL_RESPONSE { get; set; }

        public void GetRateDetails(List<PriceHelper_Product> prods, bool bDelivery)
        {
            PRICE_ENQUIRY_DETAIL_RESPONSE = InjectedPriceService.GetRatesDetail(prods, false);
        }


        public bool CommitToServiceLayer(List<Order> orderLst, List<OrderItem> orderItem, List<OrderItemExtra> extra)
        {
            return new InjectedHireReference().CommitToServiceLayer(orderLst, orderItem, extra);
        }


        public void CreateNewOrder(List<Order> order, List<OrderItem> orderItem, List<OrderItemExtra> extra)
        {
            var context = new DBMContext();            
            using (context)
            {
                context.SaveRecords<Order>(order);
                context.SaveRecords<OrderItem>(orderItem);
                context.SaveRecords<OrderItemExtra>(extra);
                CommitToServiceLayer(order, orderItem, extra);
            }
            MESSAGE = context.Message;
        }
    }
}
