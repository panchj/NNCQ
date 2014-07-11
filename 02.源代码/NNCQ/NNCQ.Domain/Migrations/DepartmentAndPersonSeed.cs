using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Migrations
{
    public class DepartmentAndPersonSeed
    {
        private static readonly EntityDbContext _DBContext = new EntityDbContext();

        public static void AddDepartment() 
        {
            var d1 = new Department() { Name = "总经办", Description = "", SortCode = "001", IsActiveDepartment = true };
            d1.ParentDapartment = d1;
            var d2 = new Department() { Name = "行政事业部", Description = "", SortCode = "002", IsActiveDepartment = true };
            d2.ParentDapartment = d2;
            var d3 = new Department() { Name = "企业规划部", Description = "", SortCode = "003", IsActiveDepartment = true };
            d3.ParentDapartment = d3;
            var d4 = new Department() { Name = "质量管理部", Description = "", SortCode = "004", IsActiveDepartment = true };
            d4.ParentDapartment = d4;
            var d5 = new Department() { Name = "人力资源部", Description = "", SortCode = "005", IsActiveDepartment = true };
            d5.ParentDapartment = d5;
            var d6 = new Department() { Name = "财务管理部", Description = "", SortCode = "006", IsActiveDepartment = true };
            d6.ParentDapartment = d6;
            var d7 = new Department() { Name = "工会", Description = "", SortCode = "007", IsActiveDepartment = true };
            d7.ParentDapartment = d7;

            var deptCollection = new List<Department>() { d1,d2,d3,d4,d5,d6,d7};
            foreach(var item in deptCollection )
                _DBContext.Departments.Add(item);
            _DBContext.SaveChanges();

        }
    }
}
