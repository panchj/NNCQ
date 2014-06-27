using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    public class Muc_LeftNavigator:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public string Title { get; set; }

        public string InnerHtmlContent{ get;set; }

        public Muc_LeftNavigator() 
        {
            ID = "divLeftNavigator";
            Name = "divLeftNavigator";
            CssClass = "";
            CssStyle = "";
        }

    }
}
