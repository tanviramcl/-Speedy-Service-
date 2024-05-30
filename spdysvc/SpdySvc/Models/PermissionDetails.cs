using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpdySvc.Models
{
    public class PermissionDetails
    {
        public int Permission_ID { get; set; }
        public bool CanApprove { get; set; }
        public bool CanCancel { get; set; }
        public bool CanViewHire { get; set; }
        public bool CanViewUser { get; set; }
        public String Role_ID { get; set; }

    }
}