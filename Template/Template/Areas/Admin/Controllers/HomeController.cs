using System.Web.Mvc;
using Template.Admin.Reusable;

namespace Template.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        [Route("", Name = "Dashboard")]
        public ActionResult Index()
        {
            return View();
        }
    }
}