using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models
{
    /// <summary>
    /// 条目外观样式规约，用于定义类似 PlainFacadeItem、SelfReferentialItem 用于下拉选项、Sidebar 环境的具体实现的一些约定。
    /// </summary>
    public enum FacadeStyle
    {
        Normal, Title, Disabled, Divider
    }
}
