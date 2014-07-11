using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Core;
using NNCQ.Domain.Organization;
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
    public class DepartmentTypeController : Controller
    {
        private readonly IEntityRepository<DepartmentType> _Service;

        public DepartmentTypeController(IEntityRepository<DepartmentType> service)
        {
            this._Service = service;
        }

        public ActionResult Index()
        {
            var boCollection = _Service.GetAll().OrderBy(s => s.SortCode);
            var boVMCollection = new List<DepartmentTypeVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new DepartmentTypeVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<DepartmentTypeVM>.GetPageMode(boVMCollection, null, null);
            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List()
        {
            var boCollection = _Service.GetAll().OrderBy(s => s.SortCode);
            var boVMCollection = new List<DepartmentTypeVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new DepartmentTypeVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<DepartmentTypeVM>.PageUpdate(boVMCollection, null, null);

            return Json(pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(Guid id)
        {
            bool isNew = false;
            var bo = _Service.GetSingle(id);
            if (bo == null)
            {
                bo = new DepartmentType();
                bo.ID = id;
                isNew = true;
            }
            var boVM = new DepartmentTypeVM(bo);
            var editor = PageComponentRepository<DepartmentTypeVM>.CreateOrEditDialog(boVM, isNew);

            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(Guid id)
        {
            var bo = _Service.GetSingle(id);
            var boVM = new DepartmentTypeVM(bo);
            var detail = PageComponentRepository<DepartmentTypeVM>.DetailDialog(boVM);
            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id)
        {
            var relevance = new List<object>()
            {
                new StatusMessageForDeleteOperation<DepartmentTypeInDepartment>("当前类型已经被数据引用，不能删除。", x=>x.DepartmentType.ID==id),
            };

            var deleteStatus = new DeleteStatus();
            BusinessEntityComponentsFactory.SetDeleteStatus<DepartmentType>(id, deleteStatus, relevance);
            var actionDeleteStatus = new DeleteActionStatus();
            actionDeleteStatus.IsOK = deleteStatus.SDSM[0].OperationStatus;
            actionDeleteStatus.ErrorMassage = deleteStatus.SDSM[0].OperationMessage;
            actionDeleteStatus.PageIndex = "1";
            actionDeleteStatus.TypeID = "";

            return Json(actionDeleteStatus);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(DepartmentTypeVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _Service.GetSingle(boVM.ID);
                if (bo == null)
                {
                    bo = new DepartmentType();
                    bo.ID = boVM.ID;
                }

                boVM.MapToBo(bo);
                _Service.AddOrEditAndSave(bo);

                return Json(PageComponentRepository<DepartmentTypeVM>.SaveOK(true, "1", ""));
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

                var editor = PageComponentRepository<DepartmentTypeVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;
                return Json(editor);
            }

        }
    }
}