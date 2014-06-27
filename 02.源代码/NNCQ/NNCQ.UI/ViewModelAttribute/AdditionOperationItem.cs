using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 附加的列表列操作导航
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AdditionOperationItem : Attribute
    {
        public string Title { get; set; }

        public AdditionOperationItem(string title) 
        {
            this.Title = title;
        }
    }
}
