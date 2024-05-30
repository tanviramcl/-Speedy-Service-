using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "OrderItemId", TABLE_NAME = "OrderItem", ERASE_FIELD = "OrderId")]
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String HireBuy { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public String ContractNumber { get; set; }
        public int? OrderStatusId { get; set; }
        public int CustomerCategoryCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
       
    }
    public class OrderDetails
    {
        //Order details
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String HireBuy { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public DateTime ContractNumber { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerCategoryCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        //Extra properties Order
        public int CustomerAccountID { get; set; }
        public String OrderPlacedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CustomerOrderNumber { get; set; }
        public String SpeedyOrderNumber { get; set; }
        public String DELCode { get; set; }
        public DateTime OnHireDate { get; set; }
        public DateTime OffHireDate { get; set; }
        public double DeliveryCharge { get; set; }
        public double TotalHire { get; set; }
        public double TotalBuy { get; set; }
        public double TotalVAT { get; set; }
        public double TotalNet { get; set; }
        public double TotalSpeedyShield { get; set; }
        public double TotalHireDeposit { get; set; }
        public double TotalHireCost { get; set; }
        public double TotalPayable { get; set; }
        public String Note { get; set; }
        public String ContactName { get; set; }
        public String ContactPhoneNumber { get; set; }
        public int DeliveryMethodId { get; set; }
        public String BranchName { get; set; }
        public String CompanyName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String County { get; set; }
        public String Postcode { get; set; }
        public int CountryID { get; set; }
        public String PhoneNumber { get; set; }
        public String MobileNumber { get; set; }
        public String FaxNumber { get; set; }
        public String Email { get; set; }
        public String AdditionalEmail { get; set; }
        public String PaymentRefNo { get; set; }
        public int PaymentMethodID { get; set; }
        public double HireLimit { get; set; }
        //
        public String OrderStatus { get; set; }
        public String FirstName { get; set; }

        //
        public String LastName { get; set; }

        public String AccountName { get; set; }


    
    }
}