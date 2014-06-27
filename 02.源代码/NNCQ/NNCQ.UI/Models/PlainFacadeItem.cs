using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models
{
    /// <summary>
    /// 普通的用于前端组件进行选项操作时，通用的规格定义
    /// </summary>
    public class PlainFacadeItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string SortCode { get; set; }
        public string OperateName { get; set; } // 显式的链接操作方法
        public string TargetType { get; set; }  // 标识单击节点后，返回的数据、页面所在的目标页或者div
        
        public bool IsActive { get; set; }
        public FacadeStyle FacadeStyle { get; set; }
        public string IconString { get; set; }
    }
}
