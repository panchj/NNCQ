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

			container.RegisterType<DbContext, EntityDbContext>();

			#region 系统基础信息设置部分
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
			container.RegisterType<IRoleStore<ApplicationRole, string>, RoleStore<ApplicationRole>>();
			container.RegisterType<IEntityRepository<ApplicationBusinessType>, EntityRepository<ApplicationBusinessType>>();
			container.RegisterType<IEntityRepository<ApplicationInformation>, EntityRepository<ApplicationInformation>>();
			container.RegisterType<IEntityRepository<AccreditRoleGroup>, EntityRepository<AccreditRoleGroup>>();
			container.RegisterType<IEntityRepository<SystemWorkPlace>, EntityRepository<SystemWorkPlace>>();
			container.RegisterType<IEntityRepository<SystemWorkSection>, EntityRepository<SystemWorkSection>>();
			container.RegisterType<IEntityRepository<SystemWorkTask>, EntityRepository<SystemWorkTask>>();
			container.RegisterType<IEntityRepository<SystemWorkTaskInRole>, EntityRepository<SystemWorkTaskInRole>>(); 
			#endregion

			#region 业务组织中人员与部门管理部分
			container.RegisterType<IEntityRepository<Department>, EntityRepository<Department>>();
			container.RegisterType<IEntityRepository<Person>, EntityRepository<Person>>();
			container.RegisterType<IEntityRepository<PersonsInDepartment>, EntityRepository<PersonsInDepartment>>();
			container.RegisterType<IEntityRepository<DepartmentLeader>, EntityRepository<DepartmentLeader>>();
			container.RegisterType<IEntityRepository<DepartmentType>, EntityRepository<DepartmentType>>();
			container.RegisterType<IEntityRepository<DepartmentTypeInDepartment>, EntityRepository<DepartmentTypeInDepartment>>();
			container.RegisterType<IEntityRepository<CredentialsType>, EntityRepository<CredentialsType>>();
			container.RegisterType<IEntityRepository<JobLevel>, EntityRepository<JobLevel>>();
			container.RegisterType<IEntityRepository<JobLevelInDepartment>, EntityRepository<JobLevelInDepartment>>();
			container.RegisterType<IEntityRepository<JobTitle>, EntityRepository<JobTitle>>();
			container.RegisterType<IEntityRepository<JobTitleInDepartment>, EntityRepository<JobTitleInDepartment>>(); 
			#endregion

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}