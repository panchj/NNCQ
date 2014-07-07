using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 授权操作角色组,当某个用户将自己所获得权限，转授给其他用户时，使用这个实体进行约束
    /// </summary>
    public class AccreditRoleGroup:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; } 
        [StringLength(1000)]
        public string Description { get; set; }            // 授权说明
        [StringLength(50)]
        public string SortCode { get; set; }

        public DateTime StartDate { get; set; }            // 授权生效日期
        public DateTime EndDate { get; set; }              // 授权结束日期

        public virtual Person Master { get; set; }                 // 授权人
        public virtual Person Accreditee { get; set; }             // 接受人
        public virtual SystemWorkTask SystemWorkTask { get; set; } // 授权任务

        public AccreditRoleGroup() 
        {
            this.ID = Guid.NewGuid();
            this.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<AccreditRoleGroup>();
        }
    }
}
