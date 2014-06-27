using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 供列表页进行列表处理底部显示页面数据的组件
    /// </summary>
    public class BottomBarPagination:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public int CurrentPage { get; set; }         // 当前页码
        public int PageAmount { get; set; }          // 当前对象集合元素的总的列表页面数量
        public int ObjectAmount { get; set; }        // 当前对象集合的总数
        public int ObjectAmountPerPage { get; set; } // 当前列表方式每夜对象数量
    }
}
