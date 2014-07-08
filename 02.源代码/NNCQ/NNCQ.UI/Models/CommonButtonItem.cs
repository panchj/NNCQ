using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models
{
    /// <summary>
    /// 规约某些特定的按钮，用于嵌入到页面组件中的指定位置
    /// </summary>
    public class CommonButtonItem
    {
        public string DisplayName { get; set; }
        public string OnClickFunction { get; set; }
        public int Width { get; set; }
    }
}
