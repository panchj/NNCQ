using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Upload(HttpPostedFileBase uploadedFile)
        {
            // Validate the uploaded file
            if (uploadedFile == null || uploadedFile.ContentType != "image/jpeg")
            {
                // Return bad request error code
                return Json(new
                {
                    statusCode = 400,
                    status = "Bad Request! Upload Failed",
                    file = string.Empty
                }, "text/html");
            }

            // Save the file to the server
            uploadedFile.SaveAs(HostingEnvironment.MapPath("/UploadFiles/" + uploadedFile.FileName));

            // Return success code
            return Json(new
            {
                statusCode = 200,
                status = "Image uploaded.",
                file = uploadedFile.FileName,
            }, "text/html");
        }

    }
}