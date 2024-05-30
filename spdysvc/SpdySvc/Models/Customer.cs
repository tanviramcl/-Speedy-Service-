using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "CustomerID", TABLE_NAME = "Customer")]
    public class Customer
    {
        public int CustomerID { get; set; }
        public String CustomerName { get; set; }
        public bool SiteLevelManager { get; set; }
        public int HireDirect { get; set; }
        public String ServiceDeskEmail { get; set; }
        public String CreditControlEmail { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool CreatedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public bool LastModifiedBy { get; set; }
        public String Location { get; set; }
        public String CustomerType { get; set; }
        public String AccountType { get; set; }
        public String VATNumber { get; set; }
        public String CompanyRegistrationNumber { get; set; }
        public String Email { get; set; }
        public String Address1 { get; set; }
        public String City { get; set; }
        public String County { get; set; }
        public String PostCode { get; set; }
        public bool CountryId { get; set; }
        public String Phone { get; set; }
        public String Mobile { get; set; }
        public String Fax { get; set; }
        public String ParentCoompanyName { get; set; }
        public String ParentCompanyRegistrationNumber { get; set; }
        public String RegisterdOfficeCity { get; set; }
        public String RegisterdOfficeCounty { get; set; }
        public String RegisterdOfficePostCode { get; set; }
        public bool   RegisterdOfficeCountryId { get; set; }
        public String RegisterdOfficePhone { get; set; }
        public String RegisterdOfficeMobile { get; set; }
        public String RegisterdOfficeFax { get; set; }
        public bool SpeedySheildType { get; set; }
        public String BankName { get; set; }
        public String BankAddress1 { get; set; }
        public String BankAddress2 { get; set; }
        public String BankCity { get; set; }
        public String BankCounty { get; set; }
        public String BankPostCode { get; set; }
        public bool BankCountryId { get; set; }
        public String BankAccountNo { get; set; }
        public String BankSortCode { get; set; }
        public bool PONumberRequired { get; set; }
        public String InsuranceCompany { get; set; }
        public DateTime PolicyExpiryDate { get; set; }
        public String PolicyNumber { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime CompanyStartDate { get; set; }
        public bool CompanyNumEmployees { get; set; }
        public string RDCORegistrationNumber { get; set; } 
        public bool BasketSessionBased { get; set; }
        public bool BasketReturnOnly { get; set; }
        public String BasketSupplierNumber { get; set; }
        public String BasketSupplierField { get; set; }
        public String BasketEndField { get; set; }
        public String BasketNullMatGroup { get; set; }
        public bool IsPunchOutCustomer { get; set; }
        public bool PerOrderLineHireDates { get; set; }
        public bool PreApproveBasket { get; set; }
        public bool DisableSalesDeliveryCharge { get; set; }
        public int PunchOutTypeID { get; set; }
        public String PunchOutSettings { get; set; }
        public int Enabled { get; set; }
       


    }

    [DataMember(ID_FIELD = "AccountID", TABLE_NAME = "CustomerAccount")]
    public class CustomerAccount
    {
        public String AccountID { get; set; }
        public String CustomerID { get; set; }
        public String AccountNum { get; set; }
        public String AccountName { get; set; }
    }
}