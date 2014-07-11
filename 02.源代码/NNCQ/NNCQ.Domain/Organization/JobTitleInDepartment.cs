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
    /// 部门内包含的工作职位
    /// </summary>
    public class JobTitleInDepartment:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; } // 在实际处理的时候，这项数据与 JobTitle 的 SortCode 一致。

        public virtual JobTitle JobTitle { get; set; }
        public virtual Department Department { get; set; }

        public JobTitleInDepartment() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<JobTitleInDepartment>();
        }

    }
}
