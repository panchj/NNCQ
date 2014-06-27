using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    public class Muc_Sidebar : IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public Muc_Sidebar() 
        {
            ID = "divBoMainArea";
            Name = "divBoMainArea";
            CssClass = "";
            CssStyle = "margin-right:10px";

        }
    }
}
