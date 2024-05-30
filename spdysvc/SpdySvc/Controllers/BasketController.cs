using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SpdySvc.Utility;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SpdySvc.Models;
using System.Web.Script.Serialization;
using SpdySvc.Provider;

namespace SpdySvc.Controllers
{
    public class BasketController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {

            ViewBag.Title = "Basket";
            List<Cart> crtLst = new List<Cart>();
            crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            ViewBag.cartItems = crtLst;
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public JsonResult add(String productMdl)
        {

            Cart carData = new JavaScriptSerializer().Deserialize<Cart>(productMdl);
            FactoryPush callFactory = new FactoryPush();

            String ProductCode = Convert.ToString(carData.ITEM_ID);
            List<Product> Products = callFactory.GetRecords<Product>(new BLSp().SP_Get_ProductDetails(ProductCode));
            var product = Products[0]; 

            carData.ProductName = product.name;
//            carData.Price = Convert.ToDouble(product.Cost);
            carData.Logo = product.file_name;

            Double totalPrice = carData.Price;
            int totaldays = Convert.ToInt32(carData.TotalDays);
            int Quantity = Convert.ToInt32(carData.Quantity);


            if (totaldays > 0)
            {
                totalPrice = carData.Price * totaldays;
            }
            if (Quantity > 0)
            {
                totalPrice = totalPrice * Quantity;
            }
            carData.TotalPrice = totalPrice;


            List<Cart> crtLst = new List<Cart>();
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }
          
            if(crtLst.FindAll(itm=>itm.ITEM_ID.Equals(carData.ITEM_ID)).Count>0)
            {
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).Quantity = carData.Quantity;
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).TotalPrice = carData.TotalPrice;
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).TotalDays = carData.TotalDays;
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).FromDate = carData.FromDate;
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).ToDate = carData.ToDate;                
            }
            else
            {
                crtLst.Add(carData);
            }
            Session[SessionVariable.CART_SESSION] = crtLst;

            return Json(crtLst, JsonRequestBehavior.AllowGet);
        }

        public Int32 total()
        {
            List<Cart> crtLst = new List<Cart>();
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }            
            return crtLst.Count;
        }

        public String remove(String id)
        {
            List<Cart> crtLst = new List<Cart>();
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }
            if (crtLst.FindAll(itm => itm.ITEM_ID.Equals(id)).Count > 0)
            {
                crtLst.Remove(crtLst.Find(itm => itm.ITEM_ID.Equals(id)));
                return "Ok";
            }
            //int total= crtLst.Count;
            return "No";
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult orderSummery()
        {
            List<Cart> crtLst = new List<Cart>();
            List<OrderSummery> ordersummery = new List<OrderSummery>();
            Double sub_total=0;
            Double vat = 0;
            
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }
            foreach (Cart cl in crtLst)
            {
                sub_total = sub_total+cl.TotalPrice;
               // vat = vat + cl.Price;
            }
            ordersummery.Add(new OrderSummery(){ SubTotal = sub_total, GrandTotal = sub_total });            
            return Json(ordersummery[0], JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult update(String basketMdl)
        {
            Cart carData = new JavaScriptSerializer().Deserialize<Cart>(basketMdl);
            //int Id = Convert.ToInt32(carData.ITEM_ID);
            String Id = carData.ITEM_ID;
            int Quantity = Convert.ToInt32(carData.Quantity);
            Double TotalPrice =0;
            List<Cart> crtLst = new List<Cart>();
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }
           
            if (crtLst.FindAll(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).Count > 0)
            {
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).Quantity = carData.Quantity;
                Double price=crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).Price;
                int  totaldays = Convert.ToInt32(crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).TotalDays);
                if (totaldays > 0)
                {
                    TotalPrice = price * totaldays;
                }
                if (Quantity > 0)
                {
                    TotalPrice = TotalPrice * Quantity;
                }
                crtLst.Find(itm => itm.ITEM_ID.Equals(carData.ITEM_ID)).TotalPrice = TotalPrice;
            }

            Session[SessionVariable.CART_SESSION] = crtLst;

            return Json(TotalPrice, JsonRequestBehavior.AllowGet);
        }

    }
}
