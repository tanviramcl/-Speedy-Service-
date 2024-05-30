using DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{

    [DataMember(ID_FIELD = "IDN", TABLE_NAME = "Token")]
    public class Token
    {
        public String IDN { get; set; }
        public String TokenId { get; set; }
        public String UserId { get; set; }
        public String Password { get; set; }
        public String Link { get; set; }
    }

    public class HireLst<T, U> 
    {
        public T _EMail;
        public U lst;
    
    }
}