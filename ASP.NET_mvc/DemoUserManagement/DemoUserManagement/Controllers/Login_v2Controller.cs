using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagement.Controllers
{
    public class Login_v2Controller : Controller
    {
        // GET: Login_v2
        public ActionResult Login_v2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login_v2(UserDetailDTO userDetailDTO)
        {
            int userID = UserLogic.GetUserID(userDetailDTO.Email, userDetailDTO.Password);

            if (userID != -1)
            {
                var isAdmin = UserLogic.IsAdmin(userID);

                UserSession sessionModel = new UserSession();
                sessionModel.UserId = userID;
                sessionModel.IsAdmin = isAdmin;
                Constants.SetSessionDetail(sessionModel);

                if (UserLogic.IsAdmin(userID))
                {
                    return RedirectToAction("UserList_v2", "UserList_v2");
                }
                return RedirectToAction("UserDetail_v2", "Registration_v2", new { UserID = userID });
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View("Login_v2", userDetailDTO);
            }
        }

        public ActionResult Logout_v2()
        {
            Session.Clear();
            return RedirectToAction("Login_v2", "Login_v2");
        }
    }
}