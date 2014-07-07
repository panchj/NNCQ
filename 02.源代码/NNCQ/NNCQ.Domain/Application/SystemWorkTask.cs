using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace NNCQ.Domain.Application
{
    /// <summary>
    /// 系统任务执行规格定义，其作用：
    /// 1. 限制角色权限：规约具体的业务操作分配到系统角色组所需要的限制，在实际使用时，缺省的系统管理角色组自然具备所有的业务操作权限；
    /// 2. 限制前端操作体现：规约具体的业务操作所对应UI界面实现时所需要的基本指引规格；
    /// 3. 限制个性化菜单：规约用于导航菜单实现的限制一般对应直接响应执行的菜单条目
    /// </summary>
    public class SystemWorkTask:IEntity
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }                       // 任务显示名称
        [StringLength(500)]
        public string Description { get; set; }                // 任务说明
        [StringLength(100)]
        public string SortCode { get; set; }                   // 任务业务编码
        [StringLength(100)]
        public string ControllerName { get; set; }             // 任务对应的模块操作功能类名称（例如 MVC 模式中的控制器名称）
        [StringLength(100)]
        public string ControllerMethod { get; set; }           // 任务对应的模块操作功能类的操作方法名称（例如 MVC 模式中的控制器的 action 方法）
        [StringLength(500)]
        public string ControllerMethodParameter { get; set; }  // 对应方法的参数，这里适用于简单的静态参数定义
        [StringLength(100)]
        public string IconName { get; set; }                   // 任务对应的提示图标
        [StringLength(100)]
        public string BusinessEntityName { get; set; }         // 任务对应的主要的业务实体名称

        public bool IsForMeOnly { get; set; }                  // 任务处理的业务对象范围是否限于用户个人
        public bool IsForMyDepartmentOnly { get; set; }        // 任务处理的业务对象是否限于用户所在部门
        public bool IsForDefaultSystemRoleGroup { get; set; }  // 任务处理的业务对象是否限于用户归属的角色
        public bool IsUsedInMenu { get; set; }                 // 是否显示在导航菜单中

        public SystemWorkTask()
        {
            this.ID = Guid.NewGuid();
            this.IconName = "";
        }
    }
}
