using System.Web.Mvc;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class SettingsController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("settings")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("settings/save")]
        public ActionResult Save()
        {
            return RedirectToAction("Index");
        }
    }
}