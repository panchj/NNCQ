using Microsoft.AspNet.Identity;
using NNCQ.Domain.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationRoleManager _RoleManager;
        private readonly ApplicationUserManager _UserManager;

        public HomeController(IRoleStore<ApplicationRole, string> roleStore, IUserStore<ApplicationUser> userStore) 
        {
            this._RoleManager = new ApplicationRoleManager(roleStore); 
            this._UserManager = new ApplicationUserManager(userStore);
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Logon(string userName, string password) 
        {
            var logonStatus = new LogonUserStatus() { IsLogon = false, Message = "<i class=' icon-warning'></i> 您输入的用户或密码错误，请检查后重新输入提交。" };
            var user = await _UserManager.FindAsync(userName, password);
            if (user != null)
            {
                logonStatus.IsLogon = true;
                logonStatus.Message = "../../Home";
            }
            return Json(logonStatus);
        }

        public string getTestString()
        {
            var htmlString = new StringBuilder();
            htmlString.Append("<div id='datepicker' class='input-control text' data-role='datepicker' data-format='yyyy-mm-dd' data-position='bottom' data-effect='fade'>");
            htmlString.Append("<input type='text'>");
            htmlString.Append("<button class='btn btn-date'></button>");
            htmlString.Append("</div>");
            return htmlString.ToString();

        }

        public class LogonUserStatus
        {
            public bool IsLogon { get; set; }
            public string Message { get; set; }
        }
    }
}