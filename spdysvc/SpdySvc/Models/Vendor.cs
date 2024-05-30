using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
namespace SpdySvc.Models
{
    [DataMember(ID_FIELD="IDN",TABLE_NAME="Vendor")]
    public class Vendor
    {
        public String IDN {get;set;}
        public String Name {get;set;}
        public String Address { get; set; }
    }
}