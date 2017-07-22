using System.Net;
using System.Web.Mvc;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplicationError()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }
    }
}