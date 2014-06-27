using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 定义列表的规格
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ListDataGridViewSpecification:Attribute
    {
        public string ControllerName { get; set; }
        public int Rows { get; set;  }               // 每页显示的行数，如果为0，则不做分页
        public string ListActionPath { get; set; }   // 列表数据操作对应的导航
        public string EditActionPath { get; set; }   // 列表中编辑操作的导航
        public string DetailActionPath { get; set; } // 列表中的明细数据的导航
        public string DeleteActionPath { get; set; } // 列表中的删除数据的导航

        public ListDataGridViewSpecification(string cName, int rows) 
        {
            this.ControllerName = cName;
            this.Rows = rows;
        }
    }
}
