using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    /*
    [DataMember(ID_FIELD="IDN",TABLE_NAME="Categorys")]
    public class Category
    {
        public String IDN { get; set; }
        public String CatName { get; set; }
        public String ReferenceKey { get; set; }
    }
    */
    [DataMember(ID_FIELD = "category_id", TABLE_NAME = "category")]
    public class Category
    {
        public Int32 category_id { get; set; }
        public Int32 parent_id { get; set; }
        public String description { get; set; }
        public String ReferenceKey { get; set; }        
        public String image { get; set; }
        public Int32 enabled { get; set; }
        public Int32 status { get; set; }
    }


    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Products")]
    public class Products
    {
        public String IDN { get; set; }
        public String Name { get; set; }
        public String Logo { get; set; }
        public String Cost { get; set; }
        public String CID { get; set; }
        public String InQty { get; set; }
    }

    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "ProductEntry")]
    public class ProductEntry 
    {
        public String IDN { get; set; }
        public String ITEM_ID { get; set; }
        public String InQty { get; set; }
        public String Unit { get; set; }
        public DateTime CreatedOn { get; set; }
        public String CreatedBy { get; set; }
    }


}