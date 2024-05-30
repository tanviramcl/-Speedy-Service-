using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
using SpdySvc.Models;
using SpdySvc.Utility;
using SpdySvc.Models.SpdyMembershipUser;
using SpdySvc.Provider;
using System.Web.Security;
namespace SpdySvc.SingleTon
{
    public class SingleTon
    {
        private static SingleTon _Instance = null;
        private SingleTon() 
        {
            
        }

        public static SingleTon Instance() 
        {
            if (_Instance == null)
                _Instance = new SingleTon();

            return _Instance;
        }
        public List<Item> ITEM_LIST { get; set; }
        public List<Role> ROLE_LIST { get; set; }
        public List<User> USER_LIST { get; set; }
        public List<MailQueue> MailLst { get; set; }
        public Authorization UserAuthorization { get; set; }
        public List<CustomerAccount> CUSTOMER_ACCOUNT { get; set; }

        public void GET_ALL_ITEM()
        {
            FactoryPush fac = new FactoryPush();
            ITEM_LIST = fac.GetRecords<Item>();

        }

        public void GET_ALL_ROLE()
        {
            FactoryPush fac = new FactoryPush();
           // ROLE_LIST = fac.GetRecords<Role>();
        }

        public void GET_ALL_USER_RECORDS()
        {
            FactoryPush fac = new FactoryPush();
            //USER_LISTs = fac.GetRecords<User>();
            USER_LIST = fac.GetRecords<User>();
         
                
        }

        public void GET_ALL_CUSTOMER_ACCOUNT() 
        {
            FactoryPush fac = new FactoryPush();
            CUSTOMER_ACCOUNT = fac.GetRecords<CustomerAccount>();
        }

        public void GET_ALL_MAIL_QUEE_RECORDS()
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            FactoryPush fac = new FactoryPush();
            MailLst = fac.GetRecords<MailQueue>();
            MailLst = MailLst.FindAll(itm => itm.Recipients.Equals((usr.Email)));
        }

        //added  by Anis 22/04/2015
       public void GetUserPermission(String UserEmail)
        {
            UserAuthorization = new Authorization();
            User usr;
            usr = SingleTon.Instance().USER_LIST.FindAll(x => x.Email == UserEmail).FirstOrDefault();
            if (usr != null)
            {

                List<PermissionDetails> lstpermissionDetails;
                FactoryPush fac = new FactoryPush();

                lstpermissionDetails = fac.GetRecords<PermissionDetails>(new BLSp().SP_GetUserPermissionDetails(usr.UserID.ToString()));

                 this.UserAuthorization.UserID =Convert.ToInt32(usr.UserID);

                if (lstpermissionDetails.Count > 0)
                {
                    PermissionDetails pd = new PermissionDetails();
                    pd = lstpermissionDetails.FirstOrDefault();

                    UserAuthorization.CanApprove = pd.CanApprove;
                    UserAuthorization.CanCancel = pd.CanCancel;
                    UserAuthorization.CanViewHire = pd.CanViewHire;
                    UserAuthorization.CanViewUser = pd.CanViewUser;

                    UserAuthorization.UserRoleID = Convert.ToInt32(pd.Role_ID);


                    if (!UserAuthorization.CanApprove)
                    {
                        UserAuthorization.AssignedUserRole = Authorization.UserRoles.Requestor;
                    }

                    if (UserAuthorization.CanApprove)
                    {
                        UserAuthorization.AssignedUserRole = Authorization.UserRoles.Approver;
                    }

                    if (UserAuthorization.CanApprove && UserAuthorization.CanCancel && UserAuthorization.CanViewHire && UserAuthorization.CanViewUser)
                    {
                        UserAuthorization.AssignedUserRole = Authorization.UserRoles.Admin;
                    }


                }

                List<Authorization.Tbl_Role> lstRoles = new List<Authorization.Tbl_Role>();
                lstRoles = fac.GetRecords<Authorization.Tbl_Role>();

                this.UserAuthorization.UserRolesList = lstRoles;
            }

            else

            {
                return;
            }

        }

    }
}