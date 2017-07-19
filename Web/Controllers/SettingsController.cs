using System.Web.Mvc;
using PersonalBlog.Web.Attributes;

namespace PersonalBlog.Web.Controllers
{
    [NoCache]
    [Authorize]
    [HandleError]
    public class SettingsController : BaseController
    {
        [Route("settings")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("settings/save")]
        public ActionResult Save()
        {
            return RedirectToAction("Index");
        }
    }
}