using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    [DataMember(ID_FIELD="IDN",TABLE_NAME="Users")]
    public class Users
    {
        public String IDN { get; set; }
        public String UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String ContactNo { get; set; }
        public String Address { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
    }   
    public class ForgetPassWord {
        public String Email { get; set; }
    
    }
    
    
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "UserRole")]
    public class UserRole 
    {
        public String IDN { get; set; }
        public String RoleID { get; set; }
        public String userID { get; set; }
    }

     [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Role")]
    public class Role
    {
        public String IDN {get;set;}
        public String RoleName {get;set;}
        public String Priority { get; set; }

    }

     public class UserRecords 
     {
         public String IDN { get; set; }
         public String UserID { get; set; }
         public String FirstName { get; set; }
         public String LastName { get; set; }
         public String ContactNo { get; set; }
         public String Address { get; set; }
         public String Password { get; set; }
         public String Email { get; set; }
         public String RoleID { get; set; }
     }
    [DataMember(ID_FIELD = "UserID", TABLE_NAME = "User")]
     public class User {
        public int? UserID { get; set; }
        public int? CustomerID { get; set; }
        public Guid? Token { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public bool? Admin { get; set; }
        public int? AdminID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String JobTitle { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String Address3 { get; set; }
        public String City { get; set; }
        public String County { get; set; }
        public int? CountryID { get; set; }
        public String PostCode { get; set; }
        public String PhoneNumber { get; set; }
        public double? PhoneDiallingCode { get; set; }
        public String PhoneExtension { get; set; }
        public String MobileNumber { get; set; }
        public String FaxNumber { get; set; }
        public int? Notes { get; set; }
        public String Division { get; set; }
        public bool? Disabled { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public int? UserType { get; set; }
        public String ReceivesMails { get; set; }
        public String Title { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? HireLimit { get; set; }
        //added by anis
        public String Role_ID { get; set; }
     
     }

    [DataMember(ID_FIELD = "UserID", TABLE_NAME = "User")]
    public class UserMdl
    {
        public int UserID { get; set; }        
        public double? HireLimit { get; set; }
        public double? LimitPercentage { get; set; }


    }


}