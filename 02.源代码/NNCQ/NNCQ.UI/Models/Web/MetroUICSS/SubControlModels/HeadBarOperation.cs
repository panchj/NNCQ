using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 规约列表页的表头部分的操作规格
    /// </summary>
    public class HeadBarOperation:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string ControllerName { get; set; }              // 控制器
        public string CreateActionPath { get; set; }            // 对应的新建按钮执行的 Action 路径
        public string SearchActionPath { get; set; }            // 搜索框提交数据对应 Action 路径

        public List<CommonButtonItem> ButtonItems { get; set; } // 附加按钮

    }
}
