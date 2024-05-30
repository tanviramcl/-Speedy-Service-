using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    public class Authorization
    {
        public UserRoles AssignedUserRole { get; set; }

        public enum UserRoles
        {
          Admin=1,
          Approver, 
          Requestor
        }
        public Authorization()
        { 
        
        }
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        public bool CanApprove { get; set; }
        public bool CanCancel { get; set; }
        public bool CanViewHire { get; set; }
        public bool CanViewUser { get; set; }
        public List<Tbl_Role> UserRolesList { get; set; }
          
        public bool HasRole(UserRoles ur)
        {
            bool hasrole = false;

            if (AssignedUserRole == ur)
                hasrole = true;

            return hasrole;
        }

        [DataMember(ID_FIELD = "Permission_ID", TABLE_NAME = "Tbl_Permission")]
        public class Tbl_Permission
        {
            public String Permission_ID { get; set; }
            public bool CanApprove { get; set; }
            public bool CanCancel { get; set; }
            public bool CanViewHire { get; set; }
            public bool CanViewUser { get; set; }
           
        }

        [DataMember(ID_FIELD = "Role_ID", TABLE_NAME = "Tbl_Role")]
        public class Tbl_Role
        {
            public String Role_ID { get; set; }
            public String Role_Name { get; set; }
            public String Role_Priority { get; set; }
        }

        
        [DataMember(ID_FIELD = "LinkRolePermission_ID", TABLE_NAME = "Tbl_RolePermission")]
        public class Tbl_RolePermission
        {
            public String LinkRolePermission_ID { get; set; }
            public String Permission_ID { get; set; }
            public String Role_ID { get; set; }
        }

        [DataMember(ID_FIELD = "UserRole_ID", TABLE_NAME = "Tbl_UserRole")]
        public class Tbl_UserRole
        {
            public String UserRole_ID { get; set; }
            public String User_ID { get; set; }
            public String Role_ID { get; set; }
        }

    }
}