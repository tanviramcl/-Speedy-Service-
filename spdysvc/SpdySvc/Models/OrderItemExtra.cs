using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "OrderItemExtraId", TABLE_NAME = "OrderItemExtra")]
    public class OrderItemExtra
    {
        public int OrderItemExtraId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String HireBuy { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public String ContractNumber { get; set; }
        public String CustomerCategoryCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ProductIndicator { get; set; }
        public int OrderItemID { get; set; }
    
    }
}