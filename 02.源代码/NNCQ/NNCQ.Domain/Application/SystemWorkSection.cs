using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 任务分工类型，用于约束在特定的工作区（子系统）内，将一般的任务进行归类定义，
    /// 在实际使用中，一般对应主菜单下的按照类型划分的子分区。
    /// </summary>
    public class SystemWorkSection:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }                         // 任务类型名称
        [StringLength(500)]
        public string Description { get; set; }                  // 任务类型说明
        [StringLength(100)]
        public string SortCode { get; set; }                     // 任务类型业务编码

        public virtual ICollection<SystemWorkTask> SystemWorkTasks { get; set; } // 任务类型包含的具体任务定义

        public SystemWorkSection()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
