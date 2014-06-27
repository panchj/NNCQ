using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 主工作区业务对象列表表格工作区
    /// </summary>
    public class Muc_MainWorkPlaceDataGridView:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string ControllerName { get; set; }   // 控制器名称
        public int rows { get; set; }                // 行数，如果数值为 0，则不分行列表
        public string EditActionPath { get; set; }   // 列表中编辑操作的导航
        public string DetailActionPath { get; set; } // 列表中的明细数据的导航
        public string DeleteActionPath { get; set; } // 列表中的删除数据的导航

        public string InnerHtmlContent { get; set; }

        public Muc_MainWorkPlaceDataGridView() 
        {
            this.CssClass = "";
            this.CssStyle = "";
        }

    }
}
