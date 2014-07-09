using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Organization
{
    public class DepartmentController:Controller
    {
        private readonly IEntityRepository<Department> _Service;

        public DepartmentController(IEntityRepository<Department> service)
        {
            this._Service = service;
        }

        public ActionResult Index() 
        {
            return View("../../Views/Admin/Common/Index");

        }
    }
}