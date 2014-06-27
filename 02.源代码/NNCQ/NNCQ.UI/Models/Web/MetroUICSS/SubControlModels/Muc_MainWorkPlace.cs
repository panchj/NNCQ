using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 主工作区业务对象列表表格
    /// </summary>
    public class Muc_MainWorkPlace:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string InnerHtmlContent { get; set; }

        public Muc_MainWorkPlaceHeadBar MainWorkPlaceHeadBar { get; set; }
        public Muc_MainWorkPlaceDataGridView MainWorkPlaceDataGridView { get; set; }
        public Muc_MainWorkPlaceBottomBar MainWorkPlaceBottomBar { get; set; }

        public Muc_MainWorkPlace() 
        {
            ID = "divBoMainArea";
            Name = "divBoMainArea";
            CssClass = "";
            CssStyle = "margin-right:10px";
        }
    }
}
