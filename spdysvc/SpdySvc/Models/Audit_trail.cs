using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;

namespace SpdySvc.Models
{
     [DataMember(ID_FIELD = "audit_trail_id", TABLE_NAME = "Audit_trail")]
    public class Audit_trail
    {
        public int audit_trail_id {get;set;}
        public int? account_id {get;set;}
        public DateTime date_time {get;set;}
        public String script {get;set;}
        public String user {get;set;}
        public String action {get;set;}
        public String table {get;set;}
        public String field {get;set;}
        public String key_value {get;set;}
        public String old_value {get;set;}
        public String new_value { get; set; }
        public String UserID { get; set; }
    }
}