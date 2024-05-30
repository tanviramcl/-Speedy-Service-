using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBManager;
using SpdySvc.SingleTon;
using System.Configuration;
namespace SpdySvc
{
    public static class KnockSingleTon
    {
        public static void AppStart()
        {
            SingleTon.SingleTon.Instance().GET_ALL_USER_RECORDS();
            SingleTon.SingleTon.Instance().GET_ALL_ITEM();
            SingleTon.SingleTon.Instance().GET_ALL_ROLE();
            SingleTon.SingleTon.Instance().GET_ALL_CUSTOMER_ACCOUNT();
          //  SingleTon.SingleTon.Instance().GetUserPermission();
        }
    }
}