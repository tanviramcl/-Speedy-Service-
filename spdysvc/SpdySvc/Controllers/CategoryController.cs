using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpdySvc.Utility;
using SpdySvc.Models;
using System.Web.Script.Serialization;
using SpdySvc.Provider;

namespace SpdySvc.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            int catId = 0;
            string catName = "";
            List<Product> products = new List<Product>();
            List<Category> cats = new List<Category>();
            if (id != null)
            {
                Session[SessionVariable.CURRENT_CAT] = id;
                // TempData[SessionVariable.ROOT_CAT_ID] = 0;

                FactoryPush callFactory = new FactoryPush();
                List<Category> cat = callFactory.GetRecords<Category>(new BLSp().SP_Get_All_Categorys("'" + id + "'", ""));
                var _cat = cat[0];
                catId = Convert.ToInt32(_cat.category_id);
                catName = _cat.description;
                Session[SessionVariable.CAT_PARENT_ID] = _cat.parent_id;
                if (_cat.parent_id == 0)
                {
                    Session[SessionVariable.ROOT_CAT_ID] = catId;
                }                
                cats = callFactory.GetRecords<Category>(new BLSp().SP_Get_All_Categorys("","'" + id + "'"));
                products = callFactory.GetRecords<Product>(new BLSp().SP_Get_Category_Products("'" + catId + "'"));
            }

            ViewBag.Title = catName;
            ViewBag.Categorys = cats;
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Product(String id = "")
        {
            
            Session[SessionVariable.CURRENT_PRODUCT] = id;
            FactoryPush callFactory = new FactoryPush();
            String ProductCode = Convert.ToString(id);
            List<Product> Products = callFactory.GetRecords<Product>(new BLSp().SP_Get_ProductDetails(ProductCode));            
            var product = Products[0]; 

            List<Cart> crtLst = new List<Cart>();
            List<Cart> basketItem = new List<Cart>();
            if (Session[SessionVariable.CART_SESSION] != null)
            {
                crtLst = (List<Cart>)Session[SessionVariable.CART_SESSION];
            }
            if (crtLst.FindAll(itm => itm.ITEM_ID.Equals(ProductCode)).Count > 0)
            {
                basketItem = crtLst.FindAll(itm => itm.ITEM_ID.Equals(ProductCode));
            }

            ViewBag.Title = "Product";
            ViewBag.Product = product;
            ViewBag.BasketItem = basketItem;
            return View();
        }


        public String GetAllCategorys()
        {

            List<Category> catList = new List<Category>();
            FactoryPush callFactory = new FactoryPush();
            //  catList = callFactory.GetRecords<Category>();
            catList = callFactory.GetRecords<Category>(new BLSp().SP_Get_All_Categorys("", ""));
            String catlink = this.createNavHtml(catList);
            return catlink;
            //return Json(catList, JsonRequestBehavior.AllowGet);
        }

        public string createNavHtml(List<Category> catList)
        {
            FactoryPush callFactory = new FactoryPush();

            string catlink = "<ul>";
            string _currentCat = (Session[SessionVariable.CURRENT_CAT] != null) ? Session[SessionVariable.CURRENT_CAT].ToString() : "";
            string _catParentId = (Session[SessionVariable.CAT_PARENT_ID] != null) ? Session[SessionVariable.CAT_PARENT_ID].ToString() : "";
            string _rootCatId = (Session[SessionVariable.ROOT_CAT_ID] != null) ? Session[SessionVariable.ROOT_CAT_ID].ToString() : "";


            int currentCat = 0;
            int catParentId = 0;
            int rootCatId = 0;
            if (_currentCat != "") { currentCat = Convert.ToInt32(_currentCat); }
            if (_catParentId != "") { catParentId = Convert.ToInt32(_catParentId); }
            if (_rootCatId != "") { rootCatId = Convert.ToInt32(_rootCatId); }


            foreach (Category cat in catList)
            {

                List<Category> subcatList = new List<Category>();

                int _catid = cat.category_id;

                string active = "";
                if (currentCat == _catid || catParentId == _catid || rootCatId == _catid)
                {
                    active = " active";
                    subcatList = callFactory.GetRecords<Category>(new BLSp().SP_Get_All_Categorys("", "'" + _catid + "'"));
                }
                if (cat.parent_id == 0)
                {
                    catlink += "<li>";
                    string catUrl = "/category/details/" + cat.category_id;
                    catlink += "<a class='list-group-item " + active + "' href='" + catUrl + "'>" + cat.description + "</a>";

                    if (subcatList.Count > 0)
                    {
                        String subCatHtml = "<ul>";
                        foreach (Category subcat in subcatList)
                        {
                            List<Category> subsubcatList = new List<Category>();
                            string activeSub = "";
                            if (currentCat == subcat.category_id || catParentId == subcat.category_id)
                            {
                                activeSub = " active";
                                subsubcatList = callFactory.GetRecords<Category>(new BLSp().SP_Get_All_Categorys("", "'" + subcat.category_id + "'"));
                            }

                            string subcatUrl = "/category/details/" + subcat.category_id;
                            subCatHtml += "<li><a class='list-group-item " + activeSub + "' href='" + subcatUrl + "'>" + subcat.description + "</a>";
                            if (subsubcatList.Count > 0)
                            {
                                String subsubCatHtml = "<ul>";
                                foreach (Category subsubcat in subsubcatList)
                                {
                                    string activeSubsub = "";
                                    if (currentCat == subsubcat.category_id)
                                    {
                                        activeSubsub = " active";

                                    }
                                    string subsubcatUrl = "/category/details/" + subsubcat.category_id;
                                    subsubCatHtml += "<li><a class='list-group-item " + activeSubsub + "' href='" + subsubcatUrl + "'>" + subsubcat.description + "</a></li>";
                                }
                                subsubCatHtml += "</ul>";
                                subCatHtml += subsubCatHtml;
                            }
                            subCatHtml += "</li>";

                        }
                        subCatHtml += "</ul>";
                        catlink += subCatHtml;
                    }
                    catlink += "</li>";
                }

            }
            catlink += "</ul>";
            return catlink;
        }

    }
}
