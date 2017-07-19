using System.Web.Mvc;
using Ninject;
using PersonalBlog.Web.Core;

namespace PersonalBlog.Web.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                UnitOfWork.BeginTransaction();
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                UnitOfWork.Commit();
            }
        }
    }
}