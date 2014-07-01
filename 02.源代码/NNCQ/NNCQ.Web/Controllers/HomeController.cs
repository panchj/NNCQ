using System;
using System.Collections.Generic;
using System.IO;
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
        public ContentResult Upload(HttpPostedFileBase file1)
        {
            var aFile = file1.FileName;

            var defaultUploadFilesUrl = Server.MapPath(Request["folder"] + "\\UploadFiles\\");
            var fileName = "" + Path.GetFileName(file1.FileName).ToLower(); ;
            var saveFile = defaultUploadFilesUrl + fileName;
            file1.SaveAs(saveFile);

            return Content("<img src='../../UploadFiles/" + fileName + "' />");
        }

    }
}