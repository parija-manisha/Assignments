using DemoUserManagement.Attributes;
using DemoUserManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagement.Controllers
{
    [CustomAuthorize]
    public class UserListController : Controller
    {
        // GET: UserList
        public ActionResult UserList()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllUsers(int start, int length, string sortColumn, string sortDirection)
        {
            
            var users = UserLogic.GetAllUsers(start, length, sortColumn, sortDirection);

            var totalRecord = UserLogic.GetTotalRecords();


            return Json(new { data = users, recordsTotal = totalRecord}, JsonRequestBehavior.AllowGet);
        }
    }
}