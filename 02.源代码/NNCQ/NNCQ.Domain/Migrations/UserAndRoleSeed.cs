using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Migrations
{
    public class UserAndRoleSeed
    {
        public static bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            var role1 = new ApplicationRole() { Name = "Admin", DisplayName = "系统管理员组", Description = "具备全部权限的用户组", SortCode = "001" };
            success = idManager.CreateRole(role1);
            if (!success == true) return success;

            var role2 = new ApplicationRole() { Name = "CanEdit", DisplayName = "业务数据编辑管理组", Description = "具备编辑一般业务数据权限的用户组", SortCode = "002" };
            success = idManager.CreateRole(role2);
            if (!success == true) return success;

            var role3 = new ApplicationRole() { Name = "User", DisplayName = "内部用户", Description = "内部操作用户组", SortCode = "003" };
            success = idManager.CreateRole(role3);
            if (!success) return success;

            var role4 = new ApplicationRole() { Name = "CustomerUser", DisplayName = "外部用户", Description = "外部操作用户组", SortCode = "004" };
            success = idManager.CreateRole(role4);
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
