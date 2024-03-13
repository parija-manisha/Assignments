using AirportFuelInventory.Business;
using AirportFuelInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AirportFuelInventory.Models.Model;

namespace AirportFuelInventory.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult NewUser(UserDTO userDTO)
        {
            bool registrationSuccess = UserLogic.NewUser(userDTO);
            if (registrationSuccess)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to register";
                return View();
            }
        }

        public JsonResult IsEmailExist(string email)
        {
            bool isEmailExist = UserLogic.IsEmailExist(email);
            return Json(isEmailExist);
        }
    }
}