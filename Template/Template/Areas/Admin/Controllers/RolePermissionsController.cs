using Service.IServices;
using Service.Properties;
using Service.Services;
using Service.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Template.Admin.Models;
using Template.Admin.Reusable;
using Template.Reusable.Extentions;

namespace Template.Areas.Admin.Controllers
{
    public class RolePermissionsController : BaseController
    {
        private static IRoleService _roleService;
        private static IPermissionService _permissionService;

        public RolePermissionsController()
        {
            _roleService = new RoleService();
            _permissionService = new PermissionService();
        }

        [Route("role-permissions", Name = "RolePermissions")]
        public ActionResult Index()
        {
            var model = new RolePermissionsViewModel
            {
                RolesGridViewModel = GetRolesGridViewModel(),
                PermissionsTreeViewModel = GetPermissionsTreeViewModel(),

                GetRolePermissionsUrl = Url.RouteUrl("GetRolePermissions"),
                UpdateRolePermissionsUrl = Url.RouteUrl("RolePermissionsUpdate")
            };

            return View(model);
        }

        [Route("role-permissions/roles/grid", Name = "RolePermissionsRolesGrid")]
        public ActionResult RolesGrid()
        {
            return PartialView("_RolesGrid", GetRolesGridViewModel());
        }

        [Route("role-permissions/permissions/tree", Name = "RolePermissionsPermissionsTree")]
        public ActionResult PermissionsTree()
        {
            return PartialView("_PermissionsTree", GetPermissionsTreeViewModel());
        }

        [Route("role-permissions/get-role-permissions", Name = "GetRolePermissions")]
        public ActionResult GetRolePermissions(int? ID)
        {
            var ajaxResponse = new AjaxResponse();

            var permissions = _roleService.GetRolePermissions(ID);

            ajaxResponse.IsSuccess = true;
            ajaxResponse.Data = new
            {
                Permissions = permissions.ToJson()
            };

            return Json(ajaxResponse);
        }

        [HttpPost]
        [Route("role-permissions/update", Name = "RolePermissionsUpdate")]
        public ActionResult RolePermissionsUpdate(int? roleID, List<int?> permissions)
        {
            var ajaxResponse = new AjaxResponse();

            _roleService.UpdateRolePermissions(roleID, permissions);

            if (_roleService.IsError)
            {
                ajaxResponse.Data = new
                {
                    Message = Resources.Abort
                };
            }
            else
            {
                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    Message = Resources.Success
                };
            }

            return Json(ajaxResponse);
        }

        private RolePermissionsViewModel.RolePermissionsRolesGridViewModel GetRolesGridViewModel()
        {
            return new RolePermissionsViewModel.RolePermissionsRolesGridViewModel
            {
                ListUrl = Url.RouteUrl("RolePermissionsRolesGrid"),
                GridItems = _roleService.GetAllGridItems().Select(r => new RolePermissionsViewModel.RolePermissionsRolesGridViewModel.RolePermissionsRoleGridItem
                {
                    ID = r.ID,
                    Caption = r.Caption
                }).ToList()
            };
        }

        private RolePermissionsViewModel.RolePermissionsPermissionsTreeViewModel GetPermissionsTreeViewModel()
        {
            return new RolePermissionsViewModel.RolePermissionsPermissionsTreeViewModel
            {
                ListUrl = Url.RouteUrl("RolePermissionsPermissionsTree"),
                TreeItems = _permissionService.GetAllTreeItems().Select(p => new RolePermissionsViewModel.RolePermissionsPermissionsTreeViewModel.RolePermissionsPermissionTreeItem
                {
                    ID = p.ID,
                    ParentID = p.ParentID,
                    Caption = p.Caption
                }).ToList()
            };
        }
    }
}