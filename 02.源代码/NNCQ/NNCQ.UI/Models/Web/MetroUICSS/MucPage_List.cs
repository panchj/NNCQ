using NNCQ.UI.Models.Web.MetroUICSS.SubControlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.Models.Web.MetroUICSS
{
    /// <summary>
    /// 规约定义一个常规的单一页面，用于一般的应用程序中，需要处理的常规的增删查改操作的应用场景
    /// </summary>
    public class MucPage_List
    {
        public bool HasLeftNavigator = true;                                           // 是否使用左侧导航列表
        public bool IsUsePageForOperation = false;                                     // 是否使用页面形式处理CURD

        public Muc_LeftNavigator LeftNavigator { get; set; }                           // 左侧导航区组件（DIV）
        public Muc_MainWorkPlace MainWorkPlace { get; set; }                           // 主工作区组件（DIV）

        public MucPage_Detail DetailPage { get; set; }                                 // 处理详细数据的页面
        public MusPage_CreateOrEdit CreateOrEditPage { get; set; }                     // 处理编辑业务对象的页面
        public MucPage_RelevanceOperation RelevanceOperationPage { get; set; }         // 处理一些特殊的关联属性数据的页面

        public MucDialogue_CreateOrEdit CreateOrEditDialogue { get; set; }             // 处理业务对象数据编辑的对话框
        public MucDialogue_Detail DetailDialogue { get; set; }                         // 处理详细对象数据的对话框
        public MucDialogue_RelevanceOperation RelevanceOperationDialogue { get; set; } // 处理一些特殊的关联属性的对话框
        public MucDialogue_Delete DeleteDialogue { get; set; }                         // 处理删除对象数据的对话框

        public string AdditionHtmlContent { get; set; }
        public string AdditionScriptContent { get; set; }
        
        // private ISpecificationValue<T> _BoVMSpecification { get; set; }

    }
}
