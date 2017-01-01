using Core.Entities;
using Service.IServices;
using Service.Services;
using Service.Utilities;
using System.Linq;
using System.Web.Mvc;
using Template.Admin.Models;
using Template.Reusable.Extentions;

namespace Template.Admin.Reusable.FilterAttributes
{
    public class BeforePageLoad : FilterAttribute, IActionFilter
    {
        private static IPermissionService _permissionService;

        public BeforePageLoad()
        {
            _permissionService = new PermissionService();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (BaseController)filterContext.Controller;
            var layoutViewModel = new LayoutViewModel();
            GetUserFromSession(filterContext, ref controller);
            UserAutorize(filterContext);
            InitMenuItems(filterContext, ref layoutViewModel, ref controller);

            controller.ViewBag.LayoutViewModel = layoutViewModel;
        }

        private void UserAutorize(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData != null && (filterContext.RouteData.Values["action"].ToString() != "Login"))
            {
                var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
                var requestedUrl = filterContext.HttpContext.Request.RawUrl;
                requestedUrl = requestedUrl == "/admin/logout" ? "/" : requestedUrl;
                if (user == null)
                {
                    filterContext.Result = new RedirectResult($"/admin/login?RedirectUrl={requestedUrl}");
                }
                else if (!user.HasUserPermission(requestedUrl))
                {
                    filterContext.Result = new RedirectResult($"/admin/login?RedirectUrl={requestedUrl}");
                }

            }
        }

        private void GetUserFromSession(ActionExecutingContext filterContext, ref BaseController contorller)
        {
            var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
            contorller.UserItem = user;
        }

        private void InitMenuItems(ActionExecutingContext filterContext, ref LayoutViewModel model, ref BaseController controller)
        {
            model.LogoutUrl = controller.Url.RouteUrl("Logout");
            var permissions = _permissionService.GetAllMenuItems().ToList();
            var requestedUrl = filterContext.HttpContext.Request.RawUrl;
            var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
            model.FirstLevelMenuItems =
                permissions.Where(p => p.ParentID == null && user.HasUserPermission(p.Url, ID: p.ID)).Select(p => new FirstLevelMenuItem
                {
                    Caption = p.Caption,
                    Url = p.Url,
                    IconName = p.IconName,
                    IsActive = p.Url == requestedUrl,
                    SecondLevelMenuItems = permissions.Where(sl => sl.ParentID == p.ID && user.HasUserPermission(sl.Url)).Select(sl => new FirstLevelMenuItem.SecondLevelMenuItem
                    {
                        Caption = sl.Caption,
                        Url = sl.Url,
                        IconName = sl.IconName,
                        IsActive = sl.Url == requestedUrl
                    }).ToList()
                }).ToList();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}