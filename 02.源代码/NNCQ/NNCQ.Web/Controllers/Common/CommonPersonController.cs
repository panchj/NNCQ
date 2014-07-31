using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using NNCQ.Domain.Utilities;
using NNCQ.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Common
{
    /// <summary>
    /// 公共的人员选择控制器
    /// </summary>
    public class CommonPersonController : Controller
    {
        private readonly IEntityRepository<Person> _PersonService;
        private readonly IEntityRepository<Department> _DepartmentService;

        public CommonPersonController(IEntityRepository<Person> pService, IEntityRepository<Department> dService) 
        {
            this._PersonService = pService;
            this._DepartmentService = dService;
        }
        public ActionResult Index()
        {
            return View("../../Views/Common/Person/Index");
        }

        /// <summary>
        /// 处理在任何与人员有关业务实体类中人员的选择
        /// </summary>
        /// <param name="keyword">会话框中的关键词</param>
        /// <param name="deptID">选择的部门的ID</param>
        /// <param name="selectedPersonID">已经选择的人员的ID</param>
        /// <param name="selectedPersonIDItem">调用本方法的保存人员ID的html组件名称或者ID</param>
        /// <param name="selectedPersonDisplayDiv">调用本方法的显示人员信息的html组件名称或者ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SinglePerson(string keyword, string deptID, string selectedPersonID,string selectedPersonIDItem, string selectedPersonDisplayDiv) 
        {
            var boCollection = _PersonService.GetAll();
            var boVMCollection = new List<PlainFacadeItem>();
            foreach (var item in boCollection) 
            {
                var boVM = new PlainFacadeItem() {ID=item.ID.ToString(), Name=item.Name, SortCode=item.SortCode };
                if (item.Department != null)
                    boVM.Description = item.Department.Name;

                boVMCollection.Add(boVM);
            }

            var tempName = "";
            if (!String.IsNullOrEmpty(selectedPersonID))
                tempName = _PersonService.GetSingle(Guid.Parse(selectedPersonID)).Name;

            ViewBag.SelectedPersonIDItem = selectedPersonIDItem;
            ViewBag.SelectedPersonDisplayDiv = selectedPersonDisplayDiv;

            ViewBag.SelectedPersonID = selectedPersonID;
            ViewBag.SelectedPersonName = tempName;

            ViewBag.PersonKeyword = keyword;

            ViewBag.Departments = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(true);
            ViewBag.SelectedDepartmentID = deptID;

            return PartialView("../../Views/Common/Person/_SinglePersonSelector", boVMCollection);
        }

        [HttpPost]
        public ActionResult SinglePersonSelectorList(string keyword, string deptID,string selectedID) 
        {
            var boCollection = new List<Person>();// _PersonService.GetAll().Take(50);
            if (!String.IsNullOrEmpty(keyword) && !String.IsNullOrEmpty(deptID))
            {
                var dID = Guid.Parse(deptID);
                var tempCollection = _PersonService.GetAll().Where(x => x.Department.ID == dID);

                boCollection = tempCollection.Where(
                    x => x.Name.Contains(keyword) ||
                        x.FirstName.Contains(keyword) ||
                        x.LastName.Contains(keyword) ||
                        x.EmployeeCode.Contains(keyword) ||
                        x.MobileNumber.Contains(keyword)
                    ).OrderBy(n => n.Name).ToList();
            }
            else 
            {
                if (String.IsNullOrEmpty(keyword) && String.IsNullOrEmpty(deptID))
                {
                    boCollection = _PersonService.GetAll().Take(50).OrderBy(n => n.Name).ToList();
                }
                else 
                {
                    if (!String.IsNullOrEmpty(keyword)) 
                    {
                        boCollection = _PersonService.GetAll().Where(
                            x => x.Name.Contains(keyword) ||
                                x.FirstName.Contains(keyword) ||
                                x.LastName.Contains(keyword) ||
                                x.EmployeeCode.Contains(keyword) ||
                                x.MobileNumber.Contains(keyword)
                            ).Take(50).OrderBy(n => n.Name).ToList();
                    }
                    if (!String.IsNullOrEmpty(deptID)) 
                    {
                        var dID = Guid.Parse(deptID);
                        boCollection = _PersonService.GetAll().Where(x => x.Department.ID == dID).OrderBy(n => n.Name).ToList(); ;
                    }
                }
            }

            var htmlContent = new StringBuilder();
            htmlContent.Append("<table class='table'>");
            foreach (var item in boCollection)
            {
                var checkedString = "";
                if (item.ID.ToString() == selectedID)
                {
                    checkedString = "checked";
                }
                var deptName = "未分配部门";
                if (item.Department != null) 
                {
                    deptName = item.Department.Name;
                }
                htmlContent.Append("<tr>");
                htmlContent.Append("<td class='text-center' style='width:50px'>");
                htmlContent.Append("<div class='input-control radio default-style' data-role='input-control' style='height:12px;margin-top:-8px'>");
                htmlContent.Append("<label>");
                htmlContent.Append("<input type='radio' name='PersonSelectorListItem' onclick='javascript: getSelectedPerson(\""+item.Name+"\",\""+item.ID+"\")'" + checkedString + "  />");
                htmlContent.Append("<span class='check'></span>");
                htmlContent.Append("</label>");
                htmlContent.Append("</td>");
                htmlContent.Append("<td class='text-left' style='width:80px'>"+item.Name+"</td>");
                htmlContent.Append("<td class='text-left'>" + deptName + "</td>");
                htmlContent.Append("</tr>");
            }
            htmlContent.Append("</table>");
            return Json(htmlContent.ToString());
        }

        
    }
}