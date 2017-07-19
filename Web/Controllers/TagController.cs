using System.Web.Mvc;
using PersonalBlog.Web.Attributes;

namespace PersonalBlog.Web.Controllers
{
    [NoCache]
    [Authorize]
    public class TagController : Controller
    {
        [Route("tags")]
        public ActionResult Index()
        {
            return View();
        }
    }
}