using NNCQ.Domain.Application;
using NNCQ.UI.Models;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNCQ.Web.ViewModels.Application
{
    [EditorSpecification("ApplicationRole", "系统角色数据", true, SaveAction = "Save", ListString = "List")]
    [ListHeadSpecification("系统角色数据定义管理",ControllerName = "ApplicationRole",SearchActionPath = "",CreateActionPath = "CreateOrEdit")]
    [ListDataGridViewSpecification("ApplicationRole", 18, ListActionPath = "List", EditActionPath = "CreateOrEdit", DetailActionPath = "Detail", DeleteActionPath = "Delete")]
    [ListNavigator("系统角色数据", "List", ListNavigatorType.SideBar)]
    //[ListHeaderAdditionalButton("Test1", "javascript:function1()",60)]
    //[ListHeaderAdditionalButton("Test2", "javascript:function2()",60)]
    [ExtensionJavaScriptFile("../../Scripts/NNCQ/nncq-ApplicaitonRole.js")]
    public class ApplicationRoleVM
    {
        [Key]
        public Guid ID { get; set; }

        [ListItemSpecification("序号", "01", 50, false)]
        public string OrderNumber { get; set; }

        [ListItemSpecification("角色名称", "02", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "角色名称不能为空值。")]
        [Display(Name = "角色名称")]
        [StringLength(50, ErrorMessage = "你输入的数据超出限制50个字符的长度。")]
        public string Name { get; set; }

        [ListItemSpecification("显示名称", "03", 200, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "显示名称不能为空值。")]
        [Display(Name = "显示名称")]
        [StringLength(150, ErrorMessage = "你输入的数据超出限制150个字符的长度。")]
        public string DisplayName { get; set; }

        [ListItemSpecification("简要说明", "04", 0, false)]
        [EditorItemSpecification(EditorItemType.TextArea, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "简要说明")]
        [StringLength(1000, ErrorMessage = "你输入的数据超出限制1000个字符的长度。")]
        public string Description { get; set; }

        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "角色编码")]
        [Required(ErrorMessage = "类型业务编码不能为空值。")]
        [StringLength(50, ErrorMessage = "你输入的数据超出限制50个字符的长度。")]
        public string SortCode { get; set; }

        [AdditionOperationItem("配置管理",130,"80")]
        public List<CommonAlinkItem> AdditionOperateUrlItems { get; set; }

        public ApplicationRoleVM() { }

        public ApplicationRoleVM(ApplicationRole bo)
        {
            this.ID = Guid.Parse(bo.Id);
            this.Name = bo.Name;
            this.DisplayName = bo.DisplayName;
            this.Description = bo.Description;
            this.SortCode = bo.SortCode;
        }

        public void MapToBo(ApplicationRole bo)
        {
            bo.Name = this.Name;
            bo.DisplayName = this.DisplayName;
            bo.Description = this.Description;
            bo.SortCode = this.SortCode;
        }

    }

}