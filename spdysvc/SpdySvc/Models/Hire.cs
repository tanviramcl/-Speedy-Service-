using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Transection")]
    public class Hire
    {
        public String IDN { get; set; }
        public String REQUESTOR_ID { get; set; }
        public String Contract { get; set; }
        public String Reference { get; set; }
        public String USER_ID { get; set; }
        public String DeliveryAddress { get; set; }
        public String TnType { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "TransectionDetails")]
    public class HireDetails
    {
        public String MID { get; set; }
        public String IDN { get; set; }
        public String ITEM_ID { get; set; }
        public String Cost { get; set; }
        public String Qty { get; set; }
        public String QtyUnit { get; set; }
        public String CreatedOn { get; set; }
        public String USER_ID { get; set; }

    }

   

}