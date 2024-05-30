using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    [DataMember(ID_FIELD="IDN",TABLE_NAME="Purchase")]
    public class Purchase
    {
        public String  IDN {get;set;}
        public String  VENDOR_ID {get;set;}
        public String PO {get;set;}
        public String Address {get;set;}

    }
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "PurchaseDetails")]
    public class PurchaseDetails
    {
        public String  IDN {get;set;}
        public String  MID {get;set;}
        public String  ITEM_ID {get;set;}
        public String  InQty {get;set;}
        public String  Unit {get;set;}
        public String CreatedUser { get; set; }
    }

}