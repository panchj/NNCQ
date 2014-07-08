using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models
{
    /// <summary>
    /// 规约某些特定的链接，用于嵌入到页面组件中的指定位置
    /// </summary>
    public class CommonAlinkItem
    {
        public string DisplayName { get; set; }     // 链接显示的字符串
        public string OnClickFunction { get; set; } // 单击链接执行的 script 函数定义
    }
}
