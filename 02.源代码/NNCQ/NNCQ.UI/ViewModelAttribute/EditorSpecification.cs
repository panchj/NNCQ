using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.UI.ViewModelAttribute
{
    /// <summary>
    /// 规约编辑的属性数据的显示的指示名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorSpecification : Attribute
    {
        public string ControllerName { get; set; } // 控制器名称
        public string Title { get; set; }          // 数据标题
        public bool UseDialogue { get; set; }      // 是否使用对话框处理数据CURD
        public string SaveAction { get; set; }     // 保存数据的 Action
        public string ListString { get; set; }     // 列表数据的 Action

        public int HorizontalZoneAmount = 1;
        public int Width = 450;
        public int Height = 300;
        public EditorSpecification(string controllerName,string title,bool dialogue) 
        {
            this.ControllerName = controllerName;
            this.Title          = title;
            this.UseDialogue    = dialogue;
        }
    }
}
