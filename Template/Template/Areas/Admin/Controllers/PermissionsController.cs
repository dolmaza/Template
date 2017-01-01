using Core.Entities;
using DevExpress.Web.Mvc;
using Service.IServices;
using Service.Properties;
using Service.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using Template.Admin.Models;
using Template.Admin.Reusable;

namespace Template.Areas.Admin.Controllers
{
    public class PermissionsController : BaseController
    {
        private static IPermissionService _permissionService;

        public PermissionsController()
        {
            _permissionService = new PermissionService();
        }

        [Route("permissions", Name = "Permissions")]
        public ActionResult Index()
        {
            var model = new PermissionsViewModel
            {
                TreeViewModel = GetTreeViewModel()
            };

            return View(model);
        }

        [Route("permissions/tree", Name = "PermissionsTree")]
        public ActionResult PermissionTree()
        {
            return PartialView("_PermissionsTree", GetTreeViewModel());
        }

        [Route("permissions/add", Name = "PermissionsAdd")]
        public ActionResult PemissionsAdd([ModelBinder(typeof(DevExpressEditorsBinder))] PermissionsViewModel.PermissionsTreeViewModel.PermissionTreeItem model)
        {
            _permissionService.Add(new Permission
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                Url = model.Url,
                Code = model.Code ?? Guid.NewGuid().ToString(),
                IsMenuItem = model.IsMenuItem,
                IconName = model.IconName,
                SortIndex = model.SortIndex
            });

            if (_permissionService.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_PermissionsTree", GetTreeViewModel());
        }

        [Route("permissions/update", Name = "PermissionsUpdate")]
        public ActionResult PemissionsUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] PermissionsViewModel.PermissionsTreeViewModel.PermissionTreeItem model)
        {
            var permission = _permissionService.GetByID(model.ID);
            if (permission == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                permission.ParentID = model.ParentID;
                permission.Caption = model.Caption;
                permission.Url = model.Url;
                permission.Code = model.Code ?? Guid.NewGuid().ToString();
                permission.IsMenuItem = model.IsMenuItem;
                permission.IconName = model.IconName;
                permission.SortIndex = model.SortIndex;

                _permissionService.Update(permission);

                if (_permissionService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }

            return PartialView("_PermissionsTree", GetTreeViewModel());
        }

        [Route("permissions/delete", Name = "PermissionsDelete")]
        public ActionResult PermissionsDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var permission = _permissionService.GetByID(ID);

            if (permission == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                _permissionService.Remove(permission);

                if (_permissionService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }


            return PartialView("_PermissionsTree", GetTreeViewModel());
        }

        private PermissionsViewModel.PermissionsTreeViewModel GetTreeViewModel()
        {
            var permissions = _permissionService.GetAllTreeItems().OrderBy(p => p.SortIndex).ToList();

            return new PermissionsViewModel.PermissionsTreeViewModel
            {
                ListUrl = Url.RouteUrl("PermissionsTree"),
                AddNewUrl = Url.RouteUrl("PermissionsAdd"),
                UpdateUrl = Url.RouteUrl("PermissionsUpdate"),
                DeleteUrl = Url.RouteUrl("PermissionsDelete"),
                TreeItems = permissions.Select(p => new PermissionsViewModel.PermissionsTreeViewModel.PermissionTreeItem
                {
                    ID = p.ID,
                    ParentID = p.ParentID,
                    Caption = p.Caption,
                    Url = p.Url,
                    Code = p.Code,
                    IconName = p.IconName,
                    IsMenuItem = p.IsMenuItem,
                    SortIndex = p.SortIndex
                }).ToList()
            };
        }
    }
}