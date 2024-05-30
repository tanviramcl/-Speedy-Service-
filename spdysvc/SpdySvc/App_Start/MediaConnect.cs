using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
using System.Configuration;
namespace SpdySvc
{
    public static class MediaConnect
    {
        public static void Connect()
        {
            MediaConnection.Instance().ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDb"].ToString();
        }
    }
}