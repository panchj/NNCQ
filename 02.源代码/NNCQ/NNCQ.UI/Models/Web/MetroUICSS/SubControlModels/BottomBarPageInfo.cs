using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 规约列表翻页操作的定义
    /// </summary>
    public class BottomBarPageInfo : IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public bool HasNextPage { get; set; }        // 是否有下一页
        public bool HasPreviousPage { get; set; }    // 是否有前一页
        public int CurrentPage { get; set; }         // 当前页码
        public int PageAmount { get; set; }          // 当前对象集合元素的总的列表页面数量
        public int StartPage { get; set; }           // 当前分页器导航显示的开始页面
        public int EndPage { get; set; }             // 当前分页器显示的终止页面
        public int PageAccount { get; set; }         // 当前分页器显示分页导航元素的个数
        public int ObjectAmountPerPage { get; set; } // 当前列表方式每夜对象数量

    }
}
