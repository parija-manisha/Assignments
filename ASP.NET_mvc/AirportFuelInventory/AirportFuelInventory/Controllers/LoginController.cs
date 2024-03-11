using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using AirportFuelInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDTO model)
        {
            int userID = UserLogic.CheckLogin(model.Email, model.Password);

            if (userID != -1)
            {
                UserSession sessionModel = new UserSession();
                sessionModel.UserId = userID;
                Constants.SetSessionDetail(sessionModel);
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
