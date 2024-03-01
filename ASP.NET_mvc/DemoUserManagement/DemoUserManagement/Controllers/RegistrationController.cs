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
    public class RegistrationController : Controller
    {
        // GET: Registration
        [CustomAuthorize]
        public ActionResult UserDetailForm(int? userId)
        {
            ViewBag.PageName = Constants.ObjectType.UserDetail;

            var countries = CountryList();
            var countryList = ((JsonResult)countries).Data as List<CountryDTO>;

            ViewBag.Countries = new SelectList(countryList, "CountryID", "CountryName");

            var documents = DocumentList();
            var documentList = ((JsonResult)documents).Data as List<DocumentTypeDTO>;

            ViewBag.Documents = new SelectList(documentList, "DocumentID", "DocumentName");

            ViewBag.States = new SelectList(new List<StateDTO>(), "StateID", "StateName");

            if (userId.HasValue)
            {
                var userDetails = UserLogic.GetUserById(userId.Value);
                ViewBag.UserDetails = userDetails;
                return View();
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetUserStates(int countryId)
        {
            var states = StateList(countryId);

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountryList()
        {
            var countries = CountryLogic.GetCountryList();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DocumentList()
        {
            var documents = Constants.DocumentType.ToList();
            return Json(documents, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StateList(int countryId)
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

                return RedirectToAction("UserDetailForm", new { UserID = userId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error in Registration, Please try again!!";
                Logger.AddError("NewUser Failed", ex);
                return View("UserDetailForm", "Registration");
            }
        }

        [HttpGet]
        public JsonResult GetUserDetails(int userId)
        {
            var user = UserLogic.GetUserById(userId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Guid.NewGuid().ToString("N") + fileExtension;

                    string uploadPath = ConfigurationManager.AppSettings["UploadDocumentPath"];
                    string filePath = Path.Combine(uploadPath, fileName);

                    file.SaveAs(filePath);

                    var response = new
                    {
                        success = true,
                        message = "File uploaded successfully",
                        fileNameOnDisk = fileName,
                        originalFileName = file.FileName,
                    };

                    FileSession fileSession = new FileSession();
                    fileSession.originalFileName = file.FileName;
                    fileSession.fileNameOnDisk = fileName;

                    Constants.SetFileSessionDetail(fileSession);

                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        success = false,
                        message = "Please select a file to upload."
                    };

                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    message = "Error uploading file: " + ex.Message
                };

                return Json(response);
            }
        }


        public JsonResult IsEmailExists(string email)
        {
            bool isEmailExist = UserLogic.IsEmailExists(email);

            return Json(isEmailExist, JsonRequestBehavior.AllowGet);
        }

    }
}
