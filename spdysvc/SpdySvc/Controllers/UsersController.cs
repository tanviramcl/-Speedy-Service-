using SpdySvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SpdySvc.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPermissionDetails()
        {
            return View();
        
        }

        public ActionResult EditUsers(String UserId)
        {

            ViewBag.UserID = UserId;
            return View();

        }


        public JsonResult UpdateUser(String editUser)
        {
            List<User> mdl = new JavaScriptSerializer().Deserialize<List<User>>(editUser);
            FactoryPush callFactory = new FactoryPush();
           // mdl[0].Admin = false;
            String msg = callFactory.EditList(mdl);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignPermission(String userId_list,String role)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> ListUserIds = serializer.Deserialize<List<int>>(userId_list);

            List<int> roles =serializer.Deserialize<List<int>>(role);

            int assignRole = roles[0];

            FactoryPush fac = new FactoryPush();

            foreach (int id in ListUserIds)
            {
                fac.ExecuteSP(new BLSp().SP_AssignUserRole(id.ToString(), assignRole.ToString()));
            }

            ViewBag.Message = "SUCCESS";
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PermissionDetails(String UserId)
        {
            ViewBag.UserID=UserId;
            return View();
        }

    }
}
