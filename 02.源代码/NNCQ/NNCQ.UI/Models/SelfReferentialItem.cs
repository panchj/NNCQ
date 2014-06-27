using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models
{
    /// <summary>
    /// 具备自我引用的节点构造模型
    /// </summary>
    public class SelfReferentialItem
    {

        public string ID { get; set; }          // 节点代表对象的ID
        public string ParentID { get; set; }    // 节点代表的业务对象的上级对象的ID
        public string ItemName { get; set; }    // 节点显示，通常就是所代表业务对象的名称
        public string OperateName { get; set; } // 显式的链接操作方法
        public string TargetType { get; set; }  // 标识单击节点后，返回的数据、页面所在的目标页或者div
        public string TipsString { get; set; }  // 帮助的指示说明
        public string SortCode { get; set; }    // 一般情况下，与业务对象的 SortCode 一致
        
        public bool IsActive { get; set; }      // 在所有的元素集合中，是否是当前选定的（一般需要在引用它的地方再做处理）
        public FacadeStyle FacadeStyle { get; set; }
        public string IconString { get; set; }

    }
}
