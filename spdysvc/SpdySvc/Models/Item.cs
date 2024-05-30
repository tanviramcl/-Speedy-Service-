using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Items")]
    public class Item
    {
        public String IDN { get; set; }
        public String Name { get; set; }
        public String Unit { get; set; }
        public String Logo { get; set; }
        public String Cost { get; set; }
        public String InQty { get; set; }
        public String CID { get; set; }
        public String Description { get; set; }

    }

    public class Product
    {
        public String product_code { get; set; }
        public String name { get; set; }
        public String category_id { get; set; }
        public String description { get; set; }
        public String Benefits { get; set; }
        public Int16 Type { get; set; }
        public Int16 AgilityID { get; set; }
        public String GoProductValue { get; set; }
        public String NewProduct { get; set; }
        public String FeaturedProduct { get; set; }
        public String BestSeller { get; set; }
        public String file_name { get; set; }
        public String caption { get; set; }
    }
}
