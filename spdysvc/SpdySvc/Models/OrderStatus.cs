using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "OrderStatusId", TABLE_NAME = "OrderStatus")]
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public String Name { get; set; }
    }
}