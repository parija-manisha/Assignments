using DemoUserManagement.Attributes;
using DemoUserManagement.Business;
using DemoUserManagement.Models;
using DemoUserManagement.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagement.Controllers
{
    public class Registration_v2Controller : Controller
    {
        // GET: UserDetail_v2
        [CustomAuthorize_v2]
        public ActionResult UserDetail_v2(int? userID)
        {
            var model = new UserDetailDTO
            {
                Countries = CountryLogic.GetCountryList(),
                DocumentType = Constants.DocumentType,
                States = new List<StateDTO>(),
            };

            if (userID.HasValue)
            {
                var user = UserLogic.GetUserById(userID.Value);

                if (user != null)
                {
                    model = user;
                }
            }

            model.Countries = model.Countries ?? new List<CountryDTO>();

            return View(model);
        }

        [HttpPost]
        public ActionResult GetStates(int countryId)
        {
            var states = StateLogic.GetStateList(countryId);

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NewUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                int userId = UserLogic.SaveUser(userDetailDTO);

                UserLogic.SaveAddress(userId, userDetailDTO);
                UserLogic.SaveRole(userId);

                var fileDetails = Constants.GetFileSessionDetail();
                if (fileDetails != null)
                {
                    string fileNameOnDisk = fileDetails.fileNameOnDisk;
                    string originalFileName = fileDetails.originalFileName;

                    int documentType = Convert.ToInt32(Request.Form["DocumentDropDown"]);
                    DocumentLogic.InsertDocument(userId, Constants.ObjectType.UserDetail, documentType, fileNameOnDisk, originalFileName);
                }
                else
                {
                    ViewBag.ErrorMessage = "Error in uploading file. Please try again!";
                }

                return RedirectToAction("UserDetail_v2", new { UserID = userId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error in Registration, Please try again!!";
                Logger.AddError("NewUser Failed", ex);
                return View("UserDetail_v2", "Registration");
            }
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var response = UserLogic.UploadFile(file);
            return Json(response);
        }

        [HttpPost]
        public ActionResult AddNote(int userId, string note, int objectType)
        {
            NoteLogic.AddNote(note, userId, objectType);
            return View();
        }

        public JsonResult IsEmailExists(string email)
        {
            bool isEmailExist = UserLogic.IsEmailExists(email);

            return Json(isEmailExist, JsonRequestBehavior.AllowGet);
        }
    }
}

