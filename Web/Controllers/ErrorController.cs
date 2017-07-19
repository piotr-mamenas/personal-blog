using System.Net;
using System.Web.Mvc;

namespace PersonalBlog.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult ApplicationError()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }
    }
}