using Service.IServices;
using Service.Properties;
using Service.Services;
using Service.Utilities;
using System.Web.Mvc;
using Template.Admin.Models;
using Template.Admin.Reusable;
using Template.Reusable.Extentions;

namespace Template.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private static IUserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

        [Route("", Name = "Dashboard")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login", Name = "Login")]
        public ActionResult Login()
        {
            var model = new LoginViewModel
            {
                LoginUrl = Url.RouteUrl("Login"),
                RedirectUrl = Request.QueryString["RedirectUrl"]
            };

            return View(model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = _userService.GetByEmailAndPassword(model.Email, model.Password?.ToMD5());

            if (user == null)
            {
                model.ErrorMessage = Resources.ValidationInvalidEmailOrPassword;
                model.Password = null;
            }
            else if (!user.IsActive)
            {
                model.ErrorMessage = Resources.ValidationInvalidEmailOrPassword;
                model.Password = null;
            }
            else
            {
                Session[AppSettings.AuthenticatedUserKey] = user;
                return Redirect(model.RedirectUrl ?? "/");
            }

            return View(model);
        }

        [Route("logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToRoute("Dashboard");
        }
    }
}