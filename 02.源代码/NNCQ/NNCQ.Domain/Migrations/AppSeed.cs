using NNCQ.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Migrations
{
    public class AppSeed
    {
        public static bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("CanEdit");
            if (!success == true) return success;

            success = idManager.CreateRole("User");
            if (!success) return success;

            success = idManager.CreateRole("CustomerUser");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "tiger",
                FirstName = "李",
                LastName = "东生",
                ChineseFullName = "李东生",
                MobileNumber = "13899998888",
                Email = "tiger@outlook.com"
            };

            // 这里注意：创建的密码要满足响应的密码规则，否则将无法创建用户。
            success = idManager.CreateUser(newUser, "123@Abc");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "CustomerUser");
            if (!success) return success;

            return success;
        }
    }
}
