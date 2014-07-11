using LNNCQ.Domain.Utilities;
using NNCQ.Domain.Application;
using NNCQ.Domain.Core;
using NNCQ.UI.Models;
using NNCQ.UI.UIModelRepository;
using NNCQ.Web.ViewModels.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Application
{
    public class SystemWorkPlaceController : Controller
    {
        private readonly IEntityRepository<SystemWorkPlace> _Service;

        public SystemWorkPlaceController(IEntityRepository<SystemWorkPlace> service) 
        {
            this._Service = service;
        }

        public ActionResult Index()
        {
            var boCollection = _Service.GetAllIncluding(x => x.SystemWorkSections).OrderBy(s => s.SortCode);
            var boVMCollection = new List<SystemWorkPlaceVM>();
            var count = 0;
            foreach (var bo in boCollection) 
            {
                var boVM = new SystemWorkPlaceVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<SystemWorkPlaceVM>.GetPageMode(boVMCollection, null, null);
            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List()
        {
            var boCollection = _Service.GetAllIncluding(x => x.SystemWorkSections).OrderBy(s => s.SortCode);
            var boVMCollection = new List<SystemWorkPlaceVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new SystemWorkPlaceVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }
            var pageModel = PageModelRepository<SystemWorkPlaceVM>.PageUpdate(boVMCollection, null, null);

            return Json(pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(Guid id)
        {
            bool isNew = false;
            var bo = _Service.GetSingle(id);
            if (bo == null)
            {
                bo = new SystemWorkPlace();
                bo.ID= id;
                isNew = true;
            }
            var boVM = new SystemWorkPlaceVM(bo);
            var editor = PageComponentRepository<SystemWorkPlaceVM>.CreateOrEditDialog(boVM, isNew);

            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(Guid id)
        {
            var bo = _Service.GetSingle(id);
            var boVM = new SystemWorkPlaceVM(bo);
            var detail = PageComponentRepository<SystemWorkPlaceVM>.DetailDialog(boVM);
            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id) 
        {
            var bo = _Service.GetSingle(id);
            var relevance = new List<object>()
            {
                new StatusMessageForDeleteOperation<SystemWorkPlace>("当前工作区已经包含有内部使用的数据，不能删除。",bo.SystemWorkSections.Count),
            };

            var deleteStatus = new DeleteStatus();
            BusinessEntityComponentsFactory.SetDeleteStatus<SystemWorkPlace>(id, deleteStatus, relevance);
           
            var actionDeleteStatus = new DeleteActionStatus();
            actionDeleteStatus.IsOK = deleteStatus.SDSM[0].OperationStatus;
            actionDeleteStatus.ErrorMassage = deleteStatus.SDSM[0].OperationMessage;
            actionDeleteStatus.PageIndex = "1";
            actionDeleteStatus.TypeID = "";

            return Json(actionDeleteStatus);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(SystemWorkPlaceVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _Service.GetSingle(boVM.ID);
                if (bo == null)
                {
                    bo = new SystemWorkPlace();
                    bo.ID = boVM.ID;
                }

                var appID = Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId;
                
                var appInfo = _Service.GetSingleRelevance<ApplicationInformation>(appID);
                if(appInfo==null)
                {
                    appInfo = new ApplicationInformation();
                    appInfo.ID = appID;
                    appInfo.AppID = appID;
                    appInfo.Name = "";
                    appInfo.Description = "";
                    appInfo.SortCode = "001";
                    _Service.AddRelevance<ApplicationInformation>(appInfo);
                    _Service.Save();
                }
                boVM.MapToBo(bo, appInfo);
                _Service.AddOrEditAndSave(bo);

                return Json(PageComponentRepository<SystemWorkPlaceVM>.SaveOK(true, "1", ""));
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

                var editor = PageComponentRepository<SystemWorkPlaceVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;
                return Json(editor);
            }

        }


    }
}