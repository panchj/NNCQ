using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Common
{
    public class BusinessImageController : Controller
    {
        // GET: BusinessImage
        public ActionResult Index()
        {
            return View("../../Views/Common/BusinessImage/Index");
        }
    }
}