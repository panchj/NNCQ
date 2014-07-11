using LNNCQ.Domain.Utilities;
using Microsoft.AspNet.Identity;
using NNCQ.Domain.Application;
using NNCQ.Domain.Core;
using NNCQ.UI.Models;
using NNCQ.UI.UIModelRepository;
using NNCQ.Web.ViewModels.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NNCQ.Web.Controllers.Application
{
    public class ApplicationRoleController : Controller
    {
        private readonly ApplicationRoleManager _RoleManager;

        public ApplicationRoleController(IRoleStore<ApplicationRole,string> store) 
        {
            _RoleManager = new ApplicationRoleManager(store);
        }

        public ActionResult Index()
        {
            var boCollection = _RoleManager.Roles.OrderBy(s=>s.SortCode);
            var boVMCollection = new List<ApplicationRoleVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new ApplicationRoleVM(bo);
                boVM.OrderNumber = (++count).ToString();
                var addColItems = new List<CommonAlinkItem>() 
                    {
                        new CommonAlinkItem() 
                        { 
                            DisplayName = "<span class=\"icon-user-3\"></span> 用户", 
                            OnClickFunction="javascript:userManager(\"" + bo.Id + "\")"
                        },
                        new CommonAlinkItem() 
                        { 
                            DisplayName = "<span class=\"icon-key\"></span> 权限", 
                            OnClickFunction="javascript:authorityManager(\"" + bo.Id + "\")" 
                        }
                    };
                boVM.AdditionOperateUrlItems = addColItems;

                boVMCollection.Add(boVM);
            }

            var pageModel = PageModelRepository<ApplicationRoleVM>.GetPageMode(boVMCollection, null, null);
            return View("../../Views/Admin/Common/Index", pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult List()
        {
            var boCollection = _RoleManager.Roles.OrderBy(s => s.SortCode);
            var boVMCollection = new List<ApplicationRoleVM>();
            var count = 0;
            foreach (var bo in boCollection)
            {
                var boVM = new ApplicationRoleVM(bo);
                boVM.OrderNumber = (++count).ToString();
                boVMCollection.Add(boVM);
            }

            var pageModel = PageModelRepository<ApplicationRoleVM>.PageUpdate(boVMCollection, null, null);

            return Json(pageModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateOrEdit(string id)
        {
            bool isNew = false;
            var bo = _RoleManager.FindById(id);
            if (bo == null)
            {
                bo = new ApplicationRole();
                bo.Id = id;
                isNew = true;
            }
            var boVM = new ApplicationRoleVM(bo);
            var editor = PageComponentRepository<ApplicationRoleVM>.CreateOrEditDialog(boVM, isNew);
            return Json(editor);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(string id)
        {
            var bo = _RoleManager.FindById(id);
            var boVM = new ApplicationRoleVM(bo);
            var detail = PageComponentRepository<ApplicationRoleVM>.DetailDialog(boVM);
            return Json(detail);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Guid id)
        {
            var deleteStatusModelItems = new List<DeleteStatusModel>();

            var role = _RoleManager.FindById(id.ToString());
            var deleteResult=_RoleManager.Delete(role);
            if (deleteResult.Succeeded)
            {
                deleteStatusModelItems.Add(new DeleteStatusModel() { OperationMessage = "", OperationStatus = true });
            }
            else 
            {
                foreach(var errorItem in deleteResult.Errors)
                {
                    deleteStatusModelItems.Add(new DeleteStatusModel() { OperationMessage = errorItem, OperationStatus = false });
                }
            }

            return Json(deleteStatusModelItems);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(ApplicationRoleVM boVM)
        {
            if (ModelState.IsValid)
            {
                var bo = _RoleManager.FindById(boVM.ID.ToString());
                var isNew = false;
                if (bo == null)
                {
                    bo = new ApplicationRole();
                    bo.Id = boVM.ID.ToString();
                    isNew = true;
                }

                boVM.MapToBo(bo);

                if (isNew)
                    _RoleManager.Create(bo);
                else
                    _RoleManager.Update(bo);

                return Json(PageComponentRepository<ApplicationRoleVM>.SaveOK(true, "1", ""));
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

                var editor = PageComponentRepository<ApplicationRoleVM>.UpdateCreateOrEditDialog(boVM, false, vItems).InnerHtmlContent;
                return Json(editor);
            }

        }

    }
}