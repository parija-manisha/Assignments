using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelInventory.Controllers
{
    public class DownloadPdfController : Controller
    {
        // GET: DownloadPdf
        public ActionResult SavePDF(string fileName, HttpPostedFileBase data)
        {
            try
            {
                string filePath = ConfigurationManager.AppSettings["PdfDirectoryPath"] + "\\" + fileName;

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                data.SaveAs(filePath);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public ActionResult OpenReport(string fileName)
        {
            try
            {
                string filePath = ConfigurationManager.AppSettings["PdfDirectoryPath"]+"\\" + fileName;
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);

                return File(fileBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

    }
}