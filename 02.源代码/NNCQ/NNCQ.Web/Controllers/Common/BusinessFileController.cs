using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Common
{
    public class BusinessFileController : Controller
    {
        public ActionResult Index()
        {
            return View("../../Views/Common/BusinessFile/Index");
        }

        [HttpPost]
        public ActionResult SingleFile(Guid objectID) 
        {
            return PartialView("../../Views/Common/BusinessImage/_BusinessImageUpload");
            //return Json(objectID);
        }

        [HttpPost]
        public ActionResult SingleFileSave() 
        {
            return Json("");
        }

        [HttpPost]
        public ActionResult MultiFiles() 
        {
            return Json("");
        }

        [HttpPost]
        public ActionResult MultiFilesSave() 
        {
            return Json("");
        }
    }
}