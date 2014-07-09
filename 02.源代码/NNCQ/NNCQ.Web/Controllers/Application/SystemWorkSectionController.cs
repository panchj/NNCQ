using NNCQ.Domain.Application;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Application
{
    public class SystemWorkSectionController:Controller
    {
        private readonly IEntityRepository<SystemWorkSection> _Service;

        public SystemWorkSectionController(IEntityRepository<SystemWorkSection> service)
        {
            this._Service = service;
        }

        public ActionResult Index() 
        {
            return View("../../Views/Admin/Common/Index");
        }
    }
}