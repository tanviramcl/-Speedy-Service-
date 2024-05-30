using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpdySvc.Spdy.Layer;
using SpdySvc.Models.SpdyMembershipUser;
using SpdySvc.Models;
using System.Web.Security;

namespace SpdySvc.Spdy.Layer
{
    public class SpdyAuthorization  
    {
       public int UserID { get; set; }//
       public int UserRoleID { get; set; }//
       public bool CanApprove { get; set; }//
       public bool CanCancel { get; set; }//
       public bool CanViewHire { get; set; }//
       public bool CanViewUser { get; set; }//

       public  UserRoles AssignedUserRole { get; set; }//

       public enum UserRoles{Admin,Approver,Requestor}//

       public bool HasRole(UserRoles ur)
        {
            bool hasrole = false;

            if (AssignedUserRole == ur)
                hasrole = true;
            
            return hasrole;
        }

        public SpdyAuthorization(SpdyMembershipUser suser)
        {
            UserID = suser.UserID;
            UserRoleID = Convert.ToInt32(suser.RoleID);
            GetUserPermission();
        }

        void GetUserPermission()
        {
            SpdyMembershipUser usr = (SpdyMembershipUser)Membership.GetUser();
            List<PermissionDetails> lstpermissionDetails;
            FactoryPush fac = new FactoryPush();

            lstpermissionDetails = fac.GetRecords<PermissionDetails>(new BLSp().SP_GetUserPermissionDetails(usr.UserID.ToString()));

            if (lstpermissionDetails.Count > 0)
            {
                PermissionDetails pd = new PermissionDetails();
                pd = lstpermissionDetails.FirstOrDefault();

                this.CanApprove = pd.CanApprove;
                this.CanCancel = pd.CanCancel;
                this.CanViewHire = pd.CanViewHire;
                this.CanViewUser = pd.CanViewUser;

                if (!CanApprove)
                {
                    AssignedUserRole = UserRoles.Requestor;
                }
                
                if (CanApprove)
                {
                    AssignedUserRole = UserRoles.Approver;
                }

                if (CanApprove && CanCancel && CanViewHire && CanViewUser)
                {
                    AssignedUserRole = UserRoles.Admin;
                }


            }
        
        }

    }
}