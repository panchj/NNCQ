using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Application;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using NNCQ.Domain.Utilities;
using NNCQ.UI.Models;
using NNCQ.UI.Models.Web.MetroUICSS;
using NNCQ.UI.UIModelRepository;
using NNCQ.Web.ViewModels.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Organization
{
    public class PersonController:Controller
    {
        private readonly IEntityRepository<Person> _Service;
        private readonly IEntityRepository<Department> _DepartmentService;
        private readonly IEntityRepository<PersonsInDepartment> _DepartmentPersonService;

        public PersonController(IEntityRepository<Person> service,IEntityRepository<Department> deparmentService, IEntityRepository<PersonsInDepartment> depPersontService)
        {
            this._Service = service;
            this._DepartmentService = deparmentService;
            this._DepartmentPersonService = depPersontService;
        }

        public ActionResult Index(string typeID) 
        {
            var pageIndex = 1;
            var pageSize = 18;

            if (String.IsNullOrEmpty(typeID)) 
            {
                typeID = _DepartmentService.GetSingleBy(x => x.ParentDapartment.ID == x.ID).ID.ToString(); 
            }
            var dID = Guid.Parse(typeID);

            var boCollection = _Service.Paginate(pageIndex, pageSize, x => x.SortCode, x => x.Department.ID == dID);
            var paginate = new MucPaginate(boCollection.PageIndex, boCollection.PageSize, boCollection.TotalCount);

            var boVMCollection = new List<PersonVM>();
            int count = 0;
            foreach (var item in boCollection) 
            {
                var boVM = new PersonVM(item);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var leftNavigatorItemCollection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(false);

            // 创建页面模型
            var pageModel = PageModelRepository<PersonVM>.GetPageMode(boVMCollection, leftNavigatorItemCollection, paginate);
            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List(string page, string keyword, string typeID) 
        {
            var pageIndex = 1;
            var pageSize = 18;

            if (!String.IsNullOrEmpty(page))
                pageIndex = int.Parse(page);

            if (keyword == null)
                keyword = "";

            Expression<Func<Person, bool>> predicate = x =>
                x.Name.Contains(keyword) ||
                x.Description.Contains(keyword) ||
                x.FirstName.Contains(keyword) ||
                x.LastName.Contains(keyword) || 
                x.MobileNumber.Contains(keyword) || 
                x.Email.Contains(keyword) || 
                x.EmployeeCode.Contains(keyword
                );

            if (!String.IsNullOrEmpty(typeID))
            {
                var tID = Guid.Parse(typeID);
                predicate = x =>
                (
                x.Name.Contains(keyword) ||
                x.Description.Contains(keyword) ||
                x.FirstName.Contains(keyword) ||
                x.LastName.Contains(keyword) || 
                x.MobileNumber.Contains(keyword) || 
                x.Email.Contains(keyword) || 
                x.EmployeeCode.Contains(keyword)
                ) && x.Department.ID == tID;
            }

            var boCollection = _Service.Paginate(pageIndex, pageSize, x => x.SortCode, predicate);
            var paginate = new MucPaginate(boCollection.PageIndex, boCollection.PageSize, boCollection.TotalCount);
            var boVMCollection = new List<PersonVM>();
            int count = 0;
            foreach (var item in boCollection)
            {
                var boVM = new PersonVM(item);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }

            var updatedPartial = PageModelRepository<PersonVM>.PageUpdate(boVMCollection, keyword, paginate);

            return Json(updatedPartial);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(Guid id)
        {
            bool isNew = false;
            var bo = _Service.GetSingle(id);
            if (bo == null)
            {
                bo = new Person();
                bo.ID = id;
                isNew = true;
            }
            var boVM = new PersonVM(bo);

            boVM.ParentItemColection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(true);
            boVM.CredentialsTypeCollection = BusinessCollectionFactory<CredentialsType>.GetPlainFacadeItemCollection();
            boVM.JobLevelCollection = BusinessCollectionFactory<JobLevel>.GetPlainFacadeItemCollection();
            boVM.JobTitleCollection = BusinessCollectionFactory<JobTitle>.GetPlainFacadeItemCollection();

            var editor = PageComponentRepository<PersonVM>.CreateOrEditDialog(boVM, isNew);
            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(Guid id)
        {
            var bo = _Service.GetSingle(id);
            var boVM = new PersonVM(bo);
            var detail = PageComponentRepository<PersonVM>.DetailDialog(boVM);
            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomDetail(Guid id) 
        {
            var bo = _Service.GetSingle(id);
            var boVM = new PersonVM(bo);
            //return Json("");
            return PartialView("../../Views/Organization/Person/_CustomDetail",boVM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id)
        {
            var typeID = _Service.GetSingle(id).Department.ID.ToString();

            // 下面的删除管理条件需要根据各个实体的业务逻辑进行定义的
            var relevance = new List<object>()
            {
                new StatusMessageForDeleteOperation<ApplicationUser>("当前人员数据已经在系统用户中关联使用，不能删除。",x=>x.Person.ID == id),
            };
            var deleteStatus = new DeleteStatus();
            BusinessEntityComponentsFactory.SetDeleteStatus<Person>(id, deleteStatus, relevance);

            var actionDeleteStatus = new DeleteActionStatus();
            actionDeleteStatus.IsOK = deleteStatus.SDSM[0].OperationStatus;
            actionDeleteStatus.ErrorMassage = deleteStatus.SDSM[0].OperationMessage;
            actionDeleteStatus.PageIndex = "1";
            actionDeleteStatus.TypeID = typeID;

            return Json(actionDeleteStatus);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(PersonVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _Service.GetSingle(boVM.ID);
                if (bo == null)
                {
                    bo = new Person();
                    bo.ID = boVM.ID;
                }

                boVM.MapToBo(bo);

                var creID = Guid.Parse(boVM.CredentialsTypeID);
                var credentialType = _Service.GetSingleRelevance<CredentialsType>(creID);

                var jlID = Guid.Parse(boVM.JobLevelID);
                var jobLevel = _Service.GetSingleRelevance<JobLevel>(jlID);

                var jtID = Guid.Parse(boVM.JobTitleID);
                var jobTitle = _Service.GetSingleRelevance<JobTitle>(jtID);

                var dID = Guid.Parse(boVM.ParentItemID);
                var dept = _Service.GetSingleRelevance<Department>(dID);

                bo.Name = bo.FirstName + bo.LastName;
                if(String.IsNullOrEmpty(bo.SortCode))
                    bo.SortCode = BusinessEntityComponentsFactory.SortCodeByDefaultDateTime<Person>();

                bo.CredentialsType = credentialType;
                bo.JobLevel = jobLevel;
                bo.JobTitle = jobTitle;
                bo.Department = dept;
                bo.UpdateTime = DateTime.Now;
                bo.IsActivePerson = true;

                _Service.AddOrEditAndSave(bo);

                //var personInDepartment = _Service.GetSingleRelevanceBy<PersonsInDepartment>(p => p.Person.ID == bo.ID && p.Department.ID == dID);
                //if (personInDepartment == null)
                //{
                //    personInDepartment = new PersonsInDepartment() { Department = dept, Person = bo };
                //    _Service.AddAndSaveRelevance<PersonsInDepartment>(personInDepartment);
                //}

                var typeID = boVM.ParentItemID;
                return Json(PageComponentRepository<PersonVM>.SaveOK(true, "1", typeID));
            }
            else
            {
                var vItems = new List<ValidatorResult>();
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors != null)
                    {
                        foreach (var vItem in item.Value.Errors)
                        {
                            var errItem = new ValidatorResult();
                            errItem.Name = item.Key;
                            errItem.ErrorMessage = vItem.ErrorMessage;
                            vItems.Add(errItem);
                        }
                    }
                }

                boVM.ParentItemColection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(true);
                boVM.CredentialsTypeCollection = BusinessCollectionFactory<CredentialsType>.GetPlainFacadeItemCollection();
                boVM.JobLevelCollection = BusinessCollectionFactory<JobLevel>.GetPlainFacadeItemCollection();
                boVM.JobTitleCollection = BusinessCollectionFactory<JobTitle>.GetPlainFacadeItemCollection();

                var editor = PageComponentRepository<PersonVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;

                return Json(editor);
            }

        }

    }
}