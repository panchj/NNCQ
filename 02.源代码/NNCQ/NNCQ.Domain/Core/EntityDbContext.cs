using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Application;
using NNCQ.Domain.Common;
using NNCQ.Domain.Files;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Core
{
    public class EntityDbContext : IdentityDbContext<ApplicationUser>
    {
        public EntityDbContext() : base("NncqContext") { }

        public IDbSet<ApplicationInformation> ApplicationInformations { get; set; }
        public IDbSet<ApplicationBusinessType> ApplicationBusinessTypes { get; set; }
        public IDbSet<ApplicaitionUserInApplication> ApplicaitionUserInApplications { get; set; }
        public IDbSet<AccreditRoleGroup> AccreditRoleGroups { get; set; }
        public IDbSet<SystemWorkPlace> SystemWorkPlaces { get; set; }
        public IDbSet<SystemWorkSection> SystemWorkSections { get; set; }
        public IDbSet<SystemWorkTask> SystemWorkTasks { get; set; }
        public IDbSet<SystemWorkTaskInRole> SystemWorkTaskInRole { get; set; }

        public IDbSet<BusinessFile> BusinessFiles { get; set; }
        public IDbSet<BusinessImage> BusinessImages { get; set; }

        public IDbSet<DepartmentType> DepartmentTypes { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<DepartmentLeader> DepartmentLeader { get; set; }
        public IDbSet<DepartmentTypeInDepartment> DepartmentTypeInDepartments { get; set; }
        public IDbSet<JobTitle> JobTitles { get; set; }
        public IDbSet<JobTitleInDepartment> JobTitleInDepartments { get; set; }
        public IDbSet<JobLevel> JobLevels { get; set; }
        public IDbSet<JobLevelInDepartment> JobLevelInDepartments { get; set; }
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<PersonsInDepartment> PersonsInDepartments { get; set; }

        public IDbSet<FileType> FileTypes { get; set; }

        public static EntityDbContext Create()
        {
            return new EntityDbContext();
        }

    }
}
