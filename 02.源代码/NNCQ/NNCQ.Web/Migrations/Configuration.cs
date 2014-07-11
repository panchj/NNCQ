namespace NNCQ.Domain.Migrations
{
    using Microsoft.AspNet.Identity;
using NNCQ.Domain.Application;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NNCQ.Domain.Core.EntityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NNCQ.Domain.Core.EntityDbContext context)
        {
            //UserAndRoleSeed.AddUserAndRoles();
            //DepartmentAndPersonSeed.AddDepartment();
        }
    }
}
