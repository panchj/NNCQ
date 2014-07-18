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

    #region 列表表头页面关联元素规格
    [ListHeadSpecification("NNCQ人员数据", ControllerName = "Person", SearchActionPath = "List", CreateActionPath = "CreateOrEdit")]
    [ListHeaderAdditionalButton("<i class='icon-enter'></i> 导入人员", "javascript:importPersonsWithExcel()", 110)]
    [ListHeaderAdditionalButton("<i class='icon-exit'></i> 导出人员", "javascript:outportPersonsWithExcel()", 110)]
    #endregion   
 
    #region 左侧导航树数据规格
    [ListNavigator("业务部门", "List", ListNavigatorType.TreeView)] 
    #endregion

    #region 列表表体内容规格定义
    [ListDataGridViewSpecification("Person", 18, ListActionPath = "List", EditActionPath = "CreateOrEdit", DetailActionPath = "Detail|javascript:personDetail", DeleteActionPath = "Delete")]
    #endregion

    #region 编辑与新建数据处理的对话框的规格定义
    [EditorSpecification("Person", "人员数据管理", true, SaveAction = "Save", ListString = "List", HorizontalZoneAmount = 2)]
    #endregion

    #region 扩展的 javascript 文件。
    [ExtensionJavaScriptFile("../../Scripts/NNCQ/nncq-Person.js")] 
    #endregion

    public class PersonVM
    {
        [Key]
        public Guid ID { get; set; }

        [ListItemSpecification("序号", "01", 50, false)]
        public string OrderNumber { get; set; }

        [EditorItemSpecification(EditorItemType.DorpdownOptionWithSelfReferentialItem, Width = 300)]
        [Required(ErrorMessage = "必须选择人员归属部门。")]
        [Display(Name = "归属部门")]
        public string ParentItemID { get; set; }
        [DetailItemSpecification(EditorItemType.SelfReferentialItem, Width = 300)]
        [Display(Name = "归属部门")]
        public SelfReferentialItem ParentItem { get; set; }
        [SelfReferentialItemSpecification("ParentItemID")]
        public List<SelfReferentialItem> ParentItemColection { get; set; }

        [ListItemSpecification("工号", "01", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "工号不能为空值。")]
        [Display(Name = "工号")]
        [StringLength(10, ErrorMessage = "你输入的数据超出限制10个字符的长度。")]
        public string EmployeeCode { get; set; }

        [ListItemSpecification("姓名", "02", 70, false)]
        [StringLength(50)]
        public string Name { get; set; }

        [EditorItemSpecification(EditorItemType.TextBox, Width = 50)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 50)]
        [Required(ErrorMessage = "姓氏不能为空值。")]
        [Display(Name = "姓氏")]
        [StringLength(6, ErrorMessage = "你输入的数据超出限制6个字符的长度。")]
        public string FirstName { get; set; }

        [EditorItemSpecification(EditorItemType.TextBox, Width = 80)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 80)]
        [Required(ErrorMessage = "名字不能为空值。")]
        [Display(Name = "名字")]
        [StringLength(6, ErrorMessage = "你输入的数据超出限制6个字符的长度。")]
        public string LastName { get; set; }

        [EditorItemSpecification(EditorItemType.ComboBox, Width = 300)]
        [Display(Name = "性别")]
        public bool Sex { get; set; }
        [ListItemSpecification("性别", "03", 50, false)]
        public string SexString { get; set; }
        [PlainFacadeItemSpecification("Sex")]
        public List<PlainFacadeItem> SexSelector { get; set; }

        [ListItemSpecification("固定电话", "04", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "固定电话")]
        [StringLength(20, ErrorMessage = "你输入的数据超出限制20个字符的长度。")]
        public string TelephoneNumber { get; set; }

        [ListItemSpecification("移动电话", "05", 150, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "移动电话不能为空值。")]
        [Display(Name = "移动电话")]
        [StringLength(20, ErrorMessage = "你输入的数据超出限制6个字符的长度。")]
        public string MobileNumber { get; set; }

        [ListItemSpecification("电子邮件", "06", 0, false)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "电子邮件不能为空值。")]
        [Display(Name = "电子邮件")]
        [StringLength(150, ErrorMessage = "你输入的数据超出限制6个字符的长度。")]
        public string Email { get; set; }

        //[EditorItemSpecification(EditorItemType.Date, Width = 300)]
        [EditorItemSpecification(EditorItemType.TextBox, Width = 300)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "出生日期")]
        public DateTime Birthday { get; set; }
        [ListItemSpecification("出生日期", "07", 80, false)]
        [Display(Name = "出生日期")]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        public string BirthdayString { get; set; }

        [EditorItemSpecification(EditorItemType.TextArea, Width = 300, HorizontalZone = 2)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Display(Name = "简要说明")]
        [StringLength(1000, ErrorMessage = "你输入的数据超出限制1000个字符的长度。")]
        public string Description { get; set; }

        [EditorItemSpecification(EditorItemType.DorpdownOptionWithPlainFacadeItem, Width = 300, HorizontalZone = 2)]
        [Required(ErrorMessage = "身份证件类型不能为空值。")]
        [Display(Name = "身份证件类型")]
        public string CredentialsTypeID { get; set; }
        [DetailItemSpecification(EditorItemType.PlainFacadeItem, Width = 300)]
        [Display(Name = "身份证件类型")]
        public PlainFacadeItem CredentialsTypeItem { get; set; }
        [PlainFacadeItemSpecification("CredentialsTypeID")]
        public List<PlainFacadeItem> CredentialsTypeCollection { get; set; }

        [EditorItemSpecification(EditorItemType.TextBox, Width = 300, HorizontalZone = 2)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "身份证件编号不能为空值。")]
        [Display(Name = "身份证件编号")]
        [StringLength(50, ErrorMessage = "你输入的数据超出限制50个字符的长度。")]
        public string CredentialsCode { get; set; }

        [EditorItemSpecification(EditorItemType.DorpdownOptionWithPlainFacadeItem, Width = 300, HorizontalZone = 2)]
        [Display(Name = "工作岗位")]
        public string JobTitleID { get; set; }
        [ListItemSpecification("工作岗位", "08", 110, false)]
        [DetailItemSpecification(EditorItemType.PlainFacadeItem, Width = 300)]
        [Display(Name = "工作岗位")]
        public PlainFacadeItem JobTitleItem { get; set; }
        [PlainFacadeItemSpecification("JobTitleID")]
        public List<PlainFacadeItem> JobTitleCollection { get; set; }

        [EditorItemSpecification(EditorItemType.DorpdownOptionWithPlainFacadeItem, Width = 300, HorizontalZone = 2)]
        [Display(Name = "岗位职级")]
        public string JobLevelID { get; set; }
        [ListItemSpecification("岗位职级", "09", 110, false)]
        [DetailItemSpecification(EditorItemType.PlainFacadeItem, Width = 300)]
        [Display(Name = "岗位职级")]
        public PlainFacadeItem JobLevelItem { get; set; }
        [PlainFacadeItemSpecification("JobLevelID")]
        public List<PlainFacadeItem> JobLevelCollection { get; set; }

        [EditorItemSpecification(EditorItemType.TextBox, Width = 300, HorizontalZone = 2)]
        [DetailItemSpecification(EditorItemType.TextBox, Width = 300)]
        [Required(ErrorMessage = "查询密码。")]
        [Display(Name = "查询密码")]
        [StringLength(50, ErrorMessage = "你输入的数据超出限制50个字符的长度。")]
        public string InquiryPassword { get; set; }

        [EditorItemSpecification(EditorItemType.Hidden, Width = 300, HorizontalZone = 2)]
        public string SortCode { get; set; }



        public PersonVM() 
        {
            this.SexSelector = new List<PlainFacadeItem>() 
            {
                new PlainFacadeItem(){ ID = "true", Name = "男" },
                new PlainFacadeItem(){ ID = "false", Name = "女" }
            };
        }

        public PersonVM(Person bo) 
        {
            this.ID              = bo.ID;
            this.Name            = bo.Name;
            this.Description     = bo.Description;
            this.SortCode        = bo.SortCode;
            this.EmployeeCode    = bo.EmployeeCode;
            this.FirstName       = bo.FirstName;
            this.LastName        = bo.LastName;
            this.Sex             = bo.Sex;
            this.TelephoneNumber = bo.TelephoneNumber;
            this.MobileNumber    = bo.MobileNumber;
            this.Email           = bo.Email;
            this.CredentialsCode = bo.CredentialsCode;
            this.Birthday        = bo.Birthday;
            this.BirthdayString  = bo.Birthday.ToShortDateString();
            this.InquiryPassword = bo.InquiryPassword;
            
            if (bo.Sex)
            {
                this.SexString = "男";
            }
            else
                this.SexString = "女";
            
            this.SexSelector = new List<PlainFacadeItem>() 
            {
                new PlainFacadeItem(){ ID = "true", Name = "男" },
                new PlainFacadeItem(){ ID = "false", Name = "女" }
            };

            if (bo.CredentialsType != null) 
            {
                this.CredentialsTypeID = bo.CredentialsType.ID.ToString();
                this.CredentialsTypeItem = new PlainFacadeItem()
                {
                    ID = bo.CredentialsType.ID.ToString(),
                    Name = bo.CredentialsType.Name,
                    SortCode = bo.CredentialsType.SortCode
                };
            }
            if (bo.JobLevel != null) 
            {
                this.JobLevelID = bo.JobLevel.ID.ToString();
                this.JobLevelItem = new PlainFacadeItem()
                {
                    ID = bo.JobLevel.ID.ToString(),
                    Name = bo.JobLevel.Name,
                    SortCode = bo.JobLevel.SortCode
                };
            }
            if (bo.JobTitle != null) 
            {
                this.JobTitleID = bo.JobTitle.ID.ToString();
                this.JobTitleItem = new PlainFacadeItem()
                {
                    ID = bo.JobTitle.ID.ToString(),
                    Name = bo.JobTitle.Name,
                    SortCode = bo.JobTitle.SortCode
                };
            }
            if (bo.Department != null) 
            {
                this.ParentItemID = bo.Department.ID.ToString();
                this.ParentItem = new SelfReferentialItem()
                {
                    ID = bo.ID.ToString(),
                    ParentID = bo.Department.ParentDapartment.ID.ToString(),
                    ItemName = bo.Name,
                    SortCode = bo.SortCode,
                    OperateName = "",
                    TargetType = "",
                    TipsString = ""
                };
            }
            
        }

        public void MapToBo(Person bo) 
        {
            bo.Name            = Name;
            bo.Description     = Description;
            bo.SortCode        = SortCode;
            bo.EmployeeCode    = EmployeeCode;
            bo.FirstName       = FirstName;
            bo.LastName        = LastName;
            bo.Sex             = Sex;
            bo.TelephoneNumber = TelephoneNumber;
            bo.MobileNumber    = MobileNumber;
            bo.Email           = Email;
            bo.CredentialsCode = CredentialsCode;
            bo.Birthday        = Birthday;
            bo.InquiryPassword = InquiryPassword;
        }
    }
}