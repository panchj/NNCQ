using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ListNavigator : Attribute
    {
        public string Title { get; set; }
        public string NavigatorAction { get; set; }
        public ListNavigatorType NavigatorType { get; set; }

        public ListNavigator(string title,string listAction,ListNavigatorType nType) 
        {
            this.Title = title;
            this.NavigatorAction = listAction;
            this.NavigatorType = nType;
        }
    }
}
