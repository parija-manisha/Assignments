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
            bool loginSuccess = UserLogic.CheckLogin(model.Email, model.Password);

            if (loginSuccess)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid credentials. Please try again.");
                return View(model);
            }
        }
    }
}
