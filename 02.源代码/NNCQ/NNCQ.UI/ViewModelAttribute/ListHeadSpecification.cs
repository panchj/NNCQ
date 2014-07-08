using NNCQ.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 用于针对需要在单个页面场景进行列表处理的常规定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ListHeadSpecification : Attribute
    {
        public string Title { get; set;  }                                        // 列表标题
        public string ControllerName { get; set; }                                // 控制器名称
        public string SearchActionPath { get; set; }                              // 如果非空使用查询功能
        public string CreateActionPath { get; set; }                              // 如果非空使用新建数据按钮

        public ListHeadSpecification(string title) 
        {
            this.Title = title;
        }

    }
}
