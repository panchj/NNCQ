using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 系统业务工作区，用于定义单个子系统的功能模块的基本规格，一般情况下对应系统主菜单条目。
    /// </summary>
    public class SystemWorkPlace:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }        // 工作区名称              
        [StringLength(100)]
        public string Description { get; set; } // 工作区说明
        [StringLength(100)]
        public string SortCode { get; set; }    // 工作区业务编码

        public virtual ApplicationInformation AppInfo { get; set; }                    // 归属的应用系统
        public virtual ICollection<SystemWorkSection> SystemWorkSections { get; set; } // 工作区内的任务分类

        public SystemWorkPlace()
        {
            this.ID = Guid.NewGuid();
        }

    }
}
