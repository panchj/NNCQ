using Microsoft.AspNet.Identity;
using NNCQ.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Application
{
    public class ApplicationUserController:Controller
    {
        private readonly ApplicationUserManager _UserManager;

        public ApplicationUserController(IUserStore<ApplicationUser> userStore) 
        {
            _UserManager = new ApplicationUserManager(userStore);
        }

        public ActionResult Index() 
        {
            return View("../../Views/Admin/Common/Index");
        }
   }
}