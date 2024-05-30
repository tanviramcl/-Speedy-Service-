using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    public class PriceHelper_Product
    {
        public int qty { get; set; }
        public string product_Code { get; set; }
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
    }
    public class ParsedPriceSummaryResponse
    {
        public String productId = "";
        public String priceType = "";

        public String buyType = "";
        public String buyRate = "";

        public String dayType = "";
        public String dayRate = "";

        public String extraDayType = "";
        public String extraDayRate = "";

        public String weekType = "";
        public String weekRate = "";

        public String minWeekType = "";
        public String minWeekRate = "";
    }

   
}