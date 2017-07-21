using System.Web.Mvc;
using Ninject;
using PersonalBlog.Web.Core;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.Interfaces;

namespace PersonalBlog.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// TODO: THIS IS NOT INJECTING AS IT SHOULD, MIGHT HAVE TO PULL IT OUT TO SOMETHING EXTERNAL
        /// </summary>
        [Inject]
        protected ILoggingService LoggingService { get; set; }

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

        protected void HandleResponse(PageResponseCode pageResponseCode, ValidationResponseCode validationResponseCode, string message)
        {
            ViewBag.Result = pageResponseCode;

            SetValidationMessage(validationResponseCode);

            ViewBag.ErrorMessage = message;
        }

        protected void HandleResponse(PageResponseCode pageResponseCode, ValidationResponseCode validationResponseCode)
        {
            ViewBag.Result = pageResponseCode;

            SetValidationMessage(validationResponseCode);
        }

        private void SetValidationMessage(ValidationResponseCode validationResponseCode)
        {
            switch (validationResponseCode)
            {
                case ValidationResponseCode.CredentialsInvalid:
                    LoggingService.Debug("Login failed. Username or Password provided was invalid");
                    ViewBag.ErrorMessage("Credentials provided invalid. Please review and try again.");
                    break;
                case ValidationResponseCode.FormInvalid:
                    ViewBag.ErrorMessage = "Form is not valid. Please review and try again.";
                    break;
            }
        }
    }
}