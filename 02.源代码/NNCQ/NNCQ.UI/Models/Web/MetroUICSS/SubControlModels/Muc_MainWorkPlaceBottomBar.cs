using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 主工作区底部工作条
    /// </summary>
    public class Muc_MainWorkPlaceBottomBar:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string InnerHtmlContent { get; set; }

        public BottomBarPageInfo BottomBarPageInfo { get; set; }
        public BottomBarPagination BottomBarPagination { get; set; }

    }
}
