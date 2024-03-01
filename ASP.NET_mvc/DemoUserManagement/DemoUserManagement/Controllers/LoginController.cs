using DemoUserManagement.Attributes;
using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
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

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            int userId = UserLogic.GetUserID(username, password);

            if (userId != -1)
            {
                var isAdmin = UserLogic.IsAdmin(userId);

                UserSession sessionModel = new UserSession();
                sessionModel.UserId = userId;
                sessionModel.IsAdmin = isAdmin;
                Constants.SetSessionDetail(sessionModel);

                if (isAdmin)
                {
                    return RedirectToAction("UserList", "UserList");
                }
                else
                {
                    return RedirectToAction("UserDetailForm","Registration", new { UserID = userId });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username password";
                return View("Login");
            }
        }

    }
}