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
        public int Width { get; set; }
        public string SortCode { get; set; }

        public AdditionOperationItem(string title,int width,string sortCode) 
        {
            this.Title = title;
            this.Width = width;
            this.SortCode = sortCode;
        }
    }
}
