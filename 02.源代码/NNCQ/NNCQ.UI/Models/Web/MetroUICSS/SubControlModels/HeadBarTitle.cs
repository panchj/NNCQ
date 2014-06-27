using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS.SubControlModels
{
    public class HeadBarTitle:IViewComponentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }
        public string CssStyle { get; set; }

        public bool HasBackAction = false;
        public string Title { get; set; }

        public HeadBarTitle() 
        {
            this.HasBackAction = false;
            this.CssClass = "";
            this.CssStyle = "";
        }
    }
}
