using NNCQ.Domain.Organization;
using NNCQ.UI.ViewModelAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNCQ.Web.ViewModels.Organization
{
    [EditorSpecification("JobLevel", "工作职级定义数据", true, SaveAction = "Save", ListString = "List")]
    [ListHeadSpecification("工作职级定义数据管理", ControllerName = "JobLevel", SearchActionPath = "", CreateActionPath = "CreateOrEdit")]
    [ListDataGridViewSpecification("JobLevel", 18, ListActionPath = "List", EditActionPath = "CreateOrEdit", DetailActionPath = "Detail", DeleteActionPath = "Delete")]
    public class JobLevelVM
    {
        [Key]
        public Guid ID { get; set; }

        [ListItemSpecification("序号", "01", 50, false)]
        public string OrderNumber { get; set; }

        [ListItemSpecification("名称", "02", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "名称不能为空值。")]
        [Display(Name = "名称")]
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

        public JobLevelVM() { }

        public JobLevelVM(JobLevel bo) 
        {
            this.ID = bo.ID;
            this.Name = bo.Name;
            this.Description = bo.Description;
            this.SortCode = bo.SortCode;
        }

        public void MapToBo(JobLevel bo) 
        {
            bo.Name = this.Name;
            bo.Description = this.Description;
            bo.SortCode = this.SortCode;
        }

    }
}