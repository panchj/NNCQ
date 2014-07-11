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
    public class CredentialsTypeController:Controller
    {
        private readonly IEntityRepository<CredentialsType> _Service;

        public CredentialsTypeController(IEntityRepository<CredentialsType> service)
        {
            this._Service = service;
        }

        public ActionResult Index()
        {
            var boCollection = _Service.GetAll().OrderBy(s => s.SortCode);
            var boVMCollection = new List<CredentialsTypeVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new CredentialsTypeVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<CredentialsTypeVM>.GetPageMode(boVMCollection, null, null);
            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List()
        {
            var boCollection = _Service.GetAll().OrderBy(s => s.SortCode);
            var boVMCollection = new List<CredentialsTypeVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new CredentialsTypeVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<CredentialsTypeVM>.PageUpdate(boVMCollection, null, null);

            return Json(pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(Guid id)
        {
            bool isNew = false;
            var bo = _Service.GetSingle(id);
            if (bo == null)
            {
                bo = new CredentialsType();
                bo.ID = id;
                isNew = true;
            }
            var boVM = new CredentialsTypeVM(bo);
            var editor = PageComponentRepository<CredentialsTypeVM>.CreateOrEditDialog(boVM, isNew);

            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(Guid id)
        {
            var bo = _Service.GetSingle(id);
            var boVM = new CredentialsTypeVM(bo);
            var detail = PageComponentRepository<CredentialsTypeVM>.DetailDialog(boVM);
            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id)
        {
            var relevance = new List<object>()
            {
                new StatusMessageForDeleteOperation<Person>("当前类型已经被人员数据引用，不能删除。", x=>x.CredentialsType.ID==id),
            };

            var deleteStatus = new DeleteStatus();
            BusinessEntityComponentsFactory.SetDeleteStatus<CredentialsType>(id, deleteStatus, relevance);
            
            var actionDeleteStatus = new DeleteActionStatus();
            actionDeleteStatus.IsOK = deleteStatus.SDSM[0].OperationStatus;
            actionDeleteStatus.ErrorMassage = deleteStatus.SDSM[0].OperationMessage;
            actionDeleteStatus.PageIndex = "1";
            actionDeleteStatus.TypeID = "";

            return Json(actionDeleteStatus);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(CredentialsTypeVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _Service.GetSingle(boVM.ID);
                if (bo == null)
                {
                    bo = new CredentialsType();
                    bo.ID = boVM.ID;
                }

                boVM.MapToBo(bo);
                _Service.AddOrEditAndSave(bo);

                return Json(PageComponentRepository<CredentialsTypeVM>.SaveOK(true, "1", ""));
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

                var editor = PageComponentRepository<CredentialsTypeVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;
                return Json(editor);
            }

        }
    }
}