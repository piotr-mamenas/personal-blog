using System.Web.Mvc;
using PersonalBlog.Web.Attributes;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [HandleError]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}