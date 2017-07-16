using System.Web.Mvc;

namespace PersonalBlog.Web.Controllers
{
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