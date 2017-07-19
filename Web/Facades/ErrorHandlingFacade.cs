using System.Security.Authentication;
using System.Web;
using PersonalBlog.Web.Core.Domain;

namespace PersonalBlog.Web.Facades
{
    public class ErrorHandlingFacade
    {
        private static ErrorHandlingFacade _instance;

        public static ErrorHandlingFacade Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorHandlingFacade();
                }
                return _instance;
            }
        }

        public BaseError GetErrorType(BaseError error)
        {
            if (error.Exception is HttpException)
            {
                var ex = error.Exception as HttpException;
                error.StatusCode = ex.GetHttpCode();

                switch (error.StatusCode)
                {
                    case 400:
                        error.RedirectAction = "BadRequest";
                        break;
                    case 401:
                        error.RedirectAction = "Unauthorized";
                        break;
                    case 403:
                        error.RedirectAction = "Forbidden";
                        break;
                    case 404:
                        error.RedirectAction = "PageNotFound";
                        break;
                    default:
                        error.RedirectAction = "ApplicationError";
                        break;
                }
            }
            else if (error.Exception is AuthenticationException)
            {
                error.RedirectAction = "Forbidden";
                error.StatusCode = 403;
            }

            return error;
        }
    }
}