using NNCQ.Domain.Organization;
using NNCQ.UI.Models;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNCQ.Web.ViewModels.Organization
{
    [EditorSpecification("Department", "部门数据", true, SaveAction = "Save", ListString = "List")]
    [ListHeadSpecification("部门管理", ControllerName = "Department", SearchActionPath = "", CreateActionPath = "CreateOrEdit")]
    [ListDataGridViewSpecification("Department", 18, ListActionPath = "List", EditActionPath = "CreateOrEdit", DetailActionPath = "Detail", DeleteActionPath = "Delete")]
    [ListNavigator("系统业务部门", "List", ListNavigatorType.TreeView)]
    //[ListHeaderAdditionalButton("Test1", "javascript:function1()",60)]
    //[ListHeaderAdditionalButton("Test2", "javascript:function2()",60)]
    [ExtensionJavaScriptFile("../../Scripts/NNCQ/nncq-Department.js")]
    public class DepartmentVM
    {
        [Key]
        public Guid ID { get; set; }

        [ListItemSpecification("序号", "01", 50, false)]
        public string OrderNumber { get; set; }

        [EditorItemSpecification(EditorItemType.DorpdownOptionWithSelfReferentialItem, Width = 300)]
        [Display(Name = "上级部门")]
        public string ParentItemID { get; set; }
        [DetailItemSpecification(EditorItemType.SelfReferentialItem, Width = 300)]
        [Display(Name = "上级部门")]
        public SelfReferentialItem ParentItem { get; set; }
        [SelfReferentialItemSpecification("ParentItemID")]
        public List<SelfReferentialItem> ParentItemColection { get; set; }

        [ListItemSpecification("部门名称", "02", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "名称不能为空值。")]
        [Display(Name = "部门名称")]
        [StringLength(100, ErrorMessage = "你输入的数据超出限制100个字符的长度。")]
        public string Name { get; set; }

        [ListItemSpecification("简要说明", "03", 0, false)]
        [EditorItemSpecification(EditorItemType.TextArea, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "简要说明")]
        [StringLength(1000, ErrorMessage = "你输入的数据超出限制1000个字符的长度。")]
        public string Description { get; set; }

        [ListItemSpecification("业务编码", "04", 100, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "业务编码")]
        [Required(ErrorMessage = "类型业务编码不能为空值。")]
        [StringLength(50, ErrorMessage = "你输入的数据超出限制50个字符的长度。")]
        public string SortCode { get; set; }

        [EditorItemSpecification(EditorItemType.ComboBox, Width = 300)]
        [Display(Name = "是否活动")]
        public bool IsActiveDepartment { get; set; }
        public string IsActiveDepartmentString { get; set; }                  
        [PlainFacadeItemSpecification("IsActiveDepartment")]
        public List<PlainFacadeItem> IsActiveDepartmentSelector { get; set; }

        [AdditionOperationItem("配置管理", 130, "80")]
        public List<CommonAlinkItem> AdditionOperateUrlItems { get; set; }


        public DepartmentVM() 
        {
            this.IsActiveDepartmentSelector = new List<PlainFacadeItem>() 
            {
                new PlainFacadeItem(){ ID = "true", Name = "是"},
                new PlainFacadeItem(){ ID = "false", Name = "否"}
            };
        }

        public DepartmentVM(Department bo) 
        {
            this.ID = bo.ID;
            this.Name = bo.Name;
            this.Description = bo.Description;
            this.SortCode = bo.SortCode;
            this.IsActiveDepartment = bo.IsActiveDepartment;
            if (bo.IsActiveDepartment)
                this.IsActiveDepartmentString = "是";
            else
                this.IsActiveDepartmentString = "否";
            
            if (bo.ParentDapartment != null)
            {
                this.ParentItemID = bo.ParentDapartment.ID.ToString();
                this.ParentItem = new SelfReferentialItem()
                {
                    ID = bo.ID.ToString(),
                    ParentID = bo.ParentDapartment.ID.ToString(),
                    ItemName = bo.ParentDapartment.Name,
                    SortCode = bo.ParentDapartment.SortCode,
                    OperateName = "",
                    TargetType = "",
                    TipsString = ""
                };
            }

            this.IsActiveDepartmentSelector = new List<PlainFacadeItem>() 
            {
                new PlainFacadeItem(){ ID = "true", Name = "是"},
                new PlainFacadeItem(){ ID = "false", Name = "否"}
            };
        }

        public void MapToBo(Department bo, Department parentBo) 
        {
            bo.Name = Name;
            bo.Description = Description;
            bo.SortCode = SortCode;
            bo.IsActiveDepartment = IsActiveDepartment;
            bo.ParentDapartment = parentBo;

        }


    }
}