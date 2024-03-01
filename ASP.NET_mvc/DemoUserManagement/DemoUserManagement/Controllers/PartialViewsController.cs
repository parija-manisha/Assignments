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
using WebGrease.Css.Ast;

namespace DemoUserManagement.Controllers
{
    public class PartialViewsController : Controller
    {
        // GET: Note
        public ActionResult Note(int pageName, int userID)
        {
            ViewData["PageName"] = pageName;
            ViewData["UserID"] = userID;

            return PartialView("~/Views/PartialViews/Note.cshtml");
        }

        [HttpGet]
        public JsonResult GetNotes(int userId, int pageName)
        {
            var note = NoteLogic.GetNotes(userId, pageName);
            return Json(note, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddNote(int userId, int pageName, string noteText)
        { 
            NoteLogic.AddNote(noteText, userId, pageName);
            return Json(new { success = true });
        }

        //GET: Document
        public ActionResult Document(int userId, int pageName)
        {
            ViewData["PageName"] = pageName;
            ViewData["UserID"] = userId;

            return PartialView("~/Views/PartialViews/Document.cshtml");
        }

        [HttpGet]
        public JsonResult GetDocuments(int userId, int pageName)
        {
            var Document = DocumentLogic.LoadDocument(userId, pageName);
            return Json(Document, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadDocument(int documentId)
        {
            var documentDetails = DocumentLogic.GetDocumentDetails(documentId);

            if (documentDetails != null)
            {
                string filePath = Path.Combine(ConfigurationManager.AppSettings["UploadDocumentPath"], documentDetails.DocumentNameOnDisk);

                if (System.IO.File.Exists(filePath))
                {
                    return File(filePath, MimeMapping.GetMimeMapping(filePath), documentDetails.DocumentOriginalName);
                }
            }
            return RedirectToAction("ErrorPage");
        }
    }
}