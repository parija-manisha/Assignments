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
    public class RegistrationController : Controller
    {
        // GET: Registration

        public ActionResult UserDetailForm(UserDetailDTO userDetailDTO)
        {
            var countries = CountryList();
            var countryList = ((JsonResult)countries).Data as List<CountryDTO>;

            ViewBag.Countries = new SelectList(countryList, "CountryID", "CountryName");

            ViewBag.States = new SelectList(new List<StateDTO>(), "Value", "Text");

            if (userDetailDTO.PermanentAddress != null && userDetailDTO.PermanentAddress.CountryID != 0)
            {
                var states = StateList(userDetailDTO.PermanentAddress.CountryID);
                var stateList = ((JsonResult)states).Data as List<StateDTO>;
                ViewBag.States = new SelectList(stateList, "StateID", "StateName");
            }

            return View();
        }



        [HttpPost]
        public ActionResult NewUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                int userId = UserLogic.SaveUser(userDetailDTO);

                return RedirectToAction("UserDetails", new { userId = userId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error in Registration, Please try again!!";
                Logger.AddError("NewUser Failed", ex);
                return View("Registration");
            }
        }

        public JsonResult CountryList()
        {
            var countries = CountryLogic.GetCountryList();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StateList(int countryId)
        {
            var states = StateLogic.GetStateList(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }
    }
}