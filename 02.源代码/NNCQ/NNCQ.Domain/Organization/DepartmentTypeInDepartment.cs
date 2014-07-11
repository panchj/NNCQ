using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    /// <summary>
    /// 部门关联的部门类型
    /// </summary>
    public class DepartmentTypeInDepartment:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }

        public virtual Department Department { get; set; }
        public virtual DepartmentType DepartmentType { get; set; }

        public DepartmentTypeInDepartment() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<DepartmentTypeInDepartment>();
        }
    }
}
