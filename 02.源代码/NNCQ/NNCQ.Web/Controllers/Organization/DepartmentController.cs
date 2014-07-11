using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
using NNCQ.Domain.Utilities;
using NNCQ.UI.Models;
using NNCQ.UI.UIModelRepository;
using NNCQ.Web.ViewModels.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Organization
{
    public class DepartmentController:Controller
    {
        private readonly IEntityRepository<Department> _Service;

        public DepartmentController(IEntityRepository<Department> service)
        {
            this._Service = service;
        }

        public ActionResult Index(string typeID) 
        {
            // 提取缺省的部门，用于约束列表中的下级部门
            var defaultDepartment = _Service.GetSingleBy(x => x.ParentDapartment.ID == x.ID);
            if (!String.IsNullOrEmpty(typeID))
            {
                var id = Guid.Parse(typeID);
                defaultDepartment = _Service.GetSingle(id);
            }
            var boCollection = _Service.FindBy(x => x.ParentDapartment.ID == defaultDepartment.ID).OrderBy(s => s.SortCode);

            // 创建部门视图模型集合
            var boVMCollection = new List<DepartmentVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new DepartmentVM(bo);
                boVM.OrderNumber = (++count).ToString();
                var addColItems = new List<CommonAlinkItem>() 
                    {
                        new CommonAlinkItem() 
                        { 
                            DisplayName = "<span class=\"icon-user-3\"></span> 部门数据配置", 
                            OnClickFunction="javascript:departmentConfig(\"" + bo.ID + "\")"
                        },
                    };
                boVM.AdditionOperateUrlItems = addColItems;

                boVMCollection.Add(boVM);
            }

            // 提取用于处理左列导航树的列表
            var leftNavigatorItemCollection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(false);

            // 创建页面模型
            var pageModel = PageModelRepository<DepartmentVM>.GetPageMode(boVMCollection, leftNavigatorItemCollection, null);

            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List(string typeID)
        {
            var tID = Guid.Parse(typeID);
            var boCollection = _Service.FindBy(x => x.ParentDapartment.ID == tID).OrderBy(s => s.SortCode);

            var boVMCollection = new List<DepartmentVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new DepartmentVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
                var addColItems = new List<CommonAlinkItem>() 
                    {
                        new CommonAlinkItem() 
                        { 
                            DisplayName = "<span class=\"icon-user-3\"></span> 部门数据配置", 
                            OnClickFunction="javascript:departmentConfig(\"" + bo.ID + "\")"
                        },
                    };
                boVM.AdditionOperateUrlItems = addColItems;
            }

            var updatedPartial = PageModelRepository<DepartmentVM>.PageUpdate(boVMCollection, null, null);

            return Json(updatedPartial);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(Guid id)
        {
            bool isNew = false;
            var bo = _Service.GetSingle(id);
            if (bo == null)
            {
                bo = new Department();
                bo.ID = id;
                isNew = true;
            }
            var boVM = new DepartmentVM(bo);

            boVM.ParentItemColection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(true);

            var editor = PageComponentRepository<DepartmentVM>.CreateOrEditDialog(boVM, isNew);
            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(Guid id)
        {
            var bo = _Service.GetSingle(id);
            var boVM = new DepartmentVM(bo);

            var detail = PageComponentRepository<DepartmentVM>.DetailDialog(boVM);

            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id) 
        {
            var typeID = _Service.GetSingle(id).ParentDapartment.ID.ToString();

            // 下面的删除管理条件需要根据各个实体的业务逻辑进行定义的
            var relevance = new List<object>()
            {
                new StatusMessageForDeleteOperation<Department>("当前部门已经被其它部门作为上级部门使用，不能删除。",x=>x.ParentDapartment.ID == id),
            };
            var deleteStatus = new DeleteStatus();
            BusinessEntityComponentsFactory.SetDeleteStatus<Department>(id, deleteStatus, relevance);

            var actionDeleteStatus = new DeleteActionStatus();
            actionDeleteStatus.IsOK = deleteStatus.SDSM[0].OperationStatus;
            actionDeleteStatus.ErrorMassage = deleteStatus.SDSM[0].OperationMessage;
            actionDeleteStatus.PageIndex = "1";
            actionDeleteStatus.TypeID = typeID;
            actionDeleteStatus.ExtenssionFunctionString = "RefreshTreeView"; // 约定数据持久化之后，除了执行返回列表的方法外，还需要执行的刷新导航树的另外的方法

            return Json(actionDeleteStatus); 
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(DepartmentVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _Service.GetSingle(boVM.ID);
                if (bo == null)
                {
                    bo = new Department();
                    bo.ID = boVM.ID;
                }
                var parentBo = bo; // 对于具有层次结构关系的实体类，如果没有选择上级层次对象，缺省处理为自身，即为根节点对象。
                if (!String.IsNullOrEmpty(boVM.ParentItemID))
                    parentBo = _Service.GetSingle(Guid.Parse(boVM.ParentItemID));

                boVM.MapToBo(bo,parentBo);

                _Service.AddOrEditAndSave(bo);

                var typeID = bo.ParentDapartment.ID.ToString();
                var extessionFunction = "RefreshTreeView"; // 约定数据持久化之后，除了执行返回列表的方法外，还需要执行的刷新导航树的另外的方法

                return Json(PageComponentRepository<DepartmentVM>.SaveOK(true, "1", typeID, extessionFunction));
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
                var editor = PageComponentRepository<DepartmentVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;
                return Json(editor);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RefreshTreeView() 
        {
            var leftNavigatorItemCollection = BusinessCollectionFactory<Department>.GetSelfReferentialItemCollection(false);
            var pageModel = PageModelRepository<DepartmentVM>.GetTreeViewNavigator(leftNavigatorItemCollection, "系统业务部门");

            return Json(pageModel.InnerHtmlContent);
        }

        public ActionResult DepartmentConfig(Guid id) 
        {
            var bo = _Service.GetSingle(id);
            var boVM = new DepartmentVM(bo);
            return View("../../Views/Organization/Department/DepartmentConfig", boVM);
        }
    }
}