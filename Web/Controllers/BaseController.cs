using System;
using System.Web.Mvc;
using Ninject;
using PersonalBlog.Web.Core;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.Interfaces;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public ILoggingService LoggingService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                UnitOfWork.BeginTransaction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                UnitOfWork.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageResponseCode"></param>
        /// <param name="validationResponseCode"></param>
        /// <param name="message"></param>
        protected void HandleResponse(PageResponseCode pageResponseCode, ValidationResponseCode validationResponseCode, string message)
        {
            ViewBag.Result = pageResponseCode;

            SetValidationMessage(validationResponseCode);

            ViewBag.ErrorMessage = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageResponseCode"></param>
        /// <param name="validationResponseCode"></param>
        protected void HandleResponse(PageResponseCode pageResponseCode, ValidationResponseCode validationResponseCode)
        {
            ViewBag.Result = pageResponseCode;

            SetValidationMessage(validationResponseCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationResponseCode"></param>
        private void SetValidationMessage(ValidationResponseCode validationResponseCode)
        {
            switch (validationResponseCode)
            {
                case ValidationResponseCode.CredentialsInvalid:
                    LoggingService.Debug("Login failed. Username or Password provided was invalid");
                    ViewBag.Message = "Credentials provided invalid. Please review and try again.";
                    break;
                case ValidationResponseCode.FormInvalid:
                    ViewBag.Message = "Form is not valid. Please review and try again.";
                    break;
                case ValidationResponseCode.ProfileChangeSuccessful:
                    ViewBag.Message = "Profile changed successfully.";
                    break;
                case ValidationResponseCode.PasswordChangeSuccessful:
                    ViewBag.Message = "Password changed successfully";
                    break;
                case ValidationResponseCode.HashtagCreateSuccessful:
                    ViewBag.Message = "Hashtag added!";
                    break;
            }
        }
    }
}