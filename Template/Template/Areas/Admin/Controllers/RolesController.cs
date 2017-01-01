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
    public class RolesController : BaseController
    {
        private static IRoleService _roleService;

        public RolesController()
        {
            _roleService = new RoleService();
        }

        [Route("roles", Name = "Roles")]
        public ActionResult Index()
        {
            var model = new RolesViewModel
            {
                GridViewModel = GetGridViewModel()
            };

            return View(model);
        }

        [Route("roles/grid", Name = "RolesGrid")]
        public ActionResult RoleGrid()
        {
            return PartialView("_RolesGrid", GetGridViewModel());
        }

        [Route("roles/add", Name = "RolesAdd")]
        public ActionResult RolesAdd([ModelBinder(typeof(DevExpressEditorsBinder))] RolesViewModel.RolesGridViewModel.RoleGridItem model)
        {
            _roleService.Add(new Role
            {
                ID = model.ID,
                Caption = model.Caption,
                Code = model.Code
            });

            if (_roleService.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_RolesGrid", GetGridViewModel());
        }

        [Route("roles/update", Name = "RolesUpdate")]
        public ActionResult RolesUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] RolesViewModel.RolesGridViewModel.RoleGridItem model)
        {
            var role = _roleService.GetByID(model.ID);

            if (role == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                role.Caption = model.Caption;
                role.Code = model.Code;

                _roleService.Update(role);

                if (_roleService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }

            return PartialView("_RolesGrid", GetGridViewModel());
        }

        [Route("roles/delete", Name = "RolesDelete")]
        public ActionResult RolesDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var role = _roleService.GetByID(ID);
            if (role == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                _roleService.Remove(role);

                if (_roleService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }

            return PartialView("_RolesGrid", GetGridViewModel());
        }

        private RolesViewModel.RolesGridViewModel GetGridViewModel()
        {
            return new RolesViewModel.RolesGridViewModel
            {
                ListUrl = Url.RouteUrl("RolesGrid"),
                AddNewUrl = Url.RouteUrl("RolesAdd"),
                UpdateUrl = Url.RouteUrl("RolesUpdate"),
                DeleteUrl = Url.RouteUrl("RolesDelete"),
                GridItems = _roleService.GetAllGridItems().Select(r => new RolesViewModel.RolesGridViewModel.RoleGridItem
                {
                    ID = r.ID,
                    Caption = r.Caption,
                    Code = r.Code
                }).ToList()
            };
        }
    }
}