using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    [DataMember(ID_FIELD = "product_code", TABLE_NAME = "product")]
    public class product
    {
        public String product_code { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public int display_order { get; set; }
        public int enabled { get; set; }
        public int status { get; set; }
        public DateTime date_created { get; set; }
        public int created_by { get; set; }
        public DateTime date_modified { get; set; }
        public int modified_by { get; set; }
        public string Benefits { get; set; }
        public int Type { get; set; }
        public int AgilityID { get; set; }
        public int hide_from_web { get; set; }
        public int RestrictFromCashHires { get; set; }
        public int GoProductValue { get; set; }
        public int NewProduct { get; set; }
        public int FeaturedProduct { get; set; }
        public int BestSeller { get; set; }
        
    }
}