using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;

namespace SpdySvc.Models
{
    public class Cart
    {
        public String ITEM_ID { get; set; }
        public String Quantity { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String UnitQty { get; set; }
        public String FromDate { get; set; }
        public String ToDate { get; set; }
        public Double Price { get; set; }
        public Double TotalPrice { get; set; }
        public String Logo { get; set; }
        public String TotalDays { get; set; }        
    }
    
    public class OrderSummery
    {
        public String OrderId { get; set; }        
        public Double SubTotal { get; set; }
        public Double GrandTotal { get; set; }
        public Double Vat { get; set; }        
    }
}