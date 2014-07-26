using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using NNCQ.Domain.Application;
using NNCQ.Domain.Files;
using NNCQ.Domain.Common;
using System.Web.Http;

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

			container.RegisterType<IEntityRepository<BusinessFile>, EntityRepository<BusinessFile>>();
			container.RegisterType<IEntityRepository<BusinessImage>, EntityRepository<BusinessImage>>();

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

			container.RegisterType<IEntityRepository<FileType>, EntityRepository<FileType>>();

            // MVC控制器和一般的类注入解析
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // WebAPI控制器注入解析
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

		}
	}
}