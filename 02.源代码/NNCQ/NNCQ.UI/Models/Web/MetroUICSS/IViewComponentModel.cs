using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS
{
    /// <summary>
    /// 普通的 Html 元素组件的基本属性规约接口
    /// </summary>
    public interface IViewComponentModel
    {
        string ID { get; set; }       // 组件ID
        string Name { get; set; }     // 组件名称
        string CssClass { get; set; } // 样式表对应的 class
        string CssStyle { get; set; } // 自定义样式补充代码
    }
}
