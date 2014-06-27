using Microsoft.AspNet.Identity.EntityFramework;
using NNCQ.Domain.Application;
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

        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<PersonsInDepartment> PersonsInDepartments { get; set; }

        public static EntityDbContext Create()
        {
            return new EntityDbContext();
        }

    }
}
