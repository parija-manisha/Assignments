using DemoUserManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagement.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        // GET: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int userId = UserLogic.GetUserID(username, password);

            if (userId != -1)
            {
                return RedirectToAction("UserDetailForm", new { UserID = userId });
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username password";
                return View("Login");
            }
        }
    }
}