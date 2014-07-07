using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using NNCQ.Domain.Application;

namespace NNCQ.Web.App_Start
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
			container.RegisterType<IRoleStore<ApplicationRole,string>, RoleStore<ApplicationRole>>();

			container.RegisterType<DbContext, EntityDbContext>();

			container.RegisterType<IEntityRepository<Department>, EntityRepository<Department>>();
			container.RegisterType<IEntityRepository<Person>, EntityRepository<Person>>();
			container.RegisterType<IEntityRepository<PersonsInDepartment>, EntityRepository<PersonsInDepartment>>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}