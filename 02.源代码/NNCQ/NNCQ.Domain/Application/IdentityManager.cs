using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 与系统用户标识认证配置相关的约束工具类
    /// </summary>
    public class IdentityManager
    {

        public bool RoleExists(string name)
        {
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new EntityDbContext()));
            return rm.RoleExists(name);
        }

        public bool CreateRole(string name,string displayName)
        {
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new EntityDbContext()));
            var role = new ApplicationRole() { Name = name, DisplayName = displayName };
            var idResult = rm.Create(role);
            return idResult.Succeeded;
        }
        public bool CreateRole(ApplicationRole role)
        {
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new EntityDbContext()));
            var idResult = rm.Create(role);
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new EntityDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new EntityDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new EntityDbContext()));
            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, role.RoleId);
            }
        }

        public bool MapUserToPerson(ApplicationUser user,Person person) 
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new EntityDbContext()));
            user.Person = person;
            var idResult = um.Update(user);
            return idResult.Succeeded;
        }

        public ApplicationRole GetRole(string roleName)
        {
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new EntityDbContext()));
            var role = rm.FindByName(roleName);
            return role;
        }

        public List<ApplicationRole> GetRoleAll() 
        {
            var results = new List<ApplicationRole>();
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new EntityDbContext()));
            foreach (var item in rm.Roles)
                results.Add(item);

            return results;
        }

        //public ApplicationUser GetUser(string userName,string password) 
        //{
        //    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new EntityDbContext()));
        //    //UserLoginInfo loginInfo=new UserLoginInfo(){ }
        //    var user = um.;
        //}

    }
}
