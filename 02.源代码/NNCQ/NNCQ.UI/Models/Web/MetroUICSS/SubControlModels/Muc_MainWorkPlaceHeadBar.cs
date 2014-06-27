using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    /// <summary>
    /// 主工作区头部显示条
    /// </summary>
    public class Muc_MainWorkPlaceHeadBar:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string InnerHtmlContent { get; set; }

        public HeadBarTitle HeadBarTitle { get; set; }
        public HeadBarOperation HeadBarOperation { get; set; }

    }
}
