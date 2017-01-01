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
using Template.Reusable.Extentions;

namespace Template.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private static IUserService _userService;
        private static IRoleService _roleService;

        public UsersController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
        }

        [Route("users", Name = "Users")]
        public ActionResult Index()
        {
            var model = new UsersViewModel
            {
                GridViewModel = GetGridViewModel()
            };

            return View(model);
        }

        [Route("users/grid", Name = "UsersGrid")]
        public ActionResult UserGrid()
        {
            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/add", Name = "UsersAdd")]
        public ActionResult UsersAdd([ModelBinder(typeof(DevExpressEditorsBinder))] UsersViewModel.UsersGridViewModel.UserGridItem model)
        {
            _userService.Add(new User
            {
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Password = model.Password?.ToMD5(),
                IsActive = model.IsActive,
                RoleID = model.RoleID
            });

            if (_userService.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/update", Name = "UsersUpdate")]
        public ActionResult UsersUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] UsersViewModel.UsersGridViewModel.UserGridItem model)
        {
            var user = _userService.GetByID(model.ID);

            if (user == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                user.ID = model.ID;
                user.Email = model.Email;
                user.Password = model.Password?.ToMD5() ?? user.Password;
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.IsActive = model.IsActive;
                user.RoleID = model.RoleID;

                _userService.Update(user);

                if (_userService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }



            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/delete", Name = "UsersDelete")]
        public ActionResult UsersDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? ID)
        {
            var user = _userService.GetByID(ID);

            if (user == null)
            {
                throw new Exception(Resources.Abort);
            }

            _userService.Remove(user);

            if (_userService.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UsersGrid", GetGridViewModel());
        }

        private UsersViewModel.UsersGridViewModel GetGridViewModel()
        {

            return new UsersViewModel.UsersGridViewModel
            {
                ListUrl = Url.RouteUrl("UsersGrid"),
                AddNewUrl = Url.RouteUrl("UsersAdd"),
                UpdateUrl = Url.RouteUrl("UsersUpdate"),
                DeleteUrl = Url.RouteUrl("UsersDelete"),
                GridItems = _userService.GetAllGridItems().Select(u => new UsersViewModel.UsersGridViewModel.UserGridItem
                {
                    ID = u.ID,
                    Email = u.Email,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    IsActive = u.IsActive,
                    RoleID = u.RoleID
                }).ToList(),

                Roles = _roleService.GetAllDropDownItems().ToList()
            };
        }
    }
}