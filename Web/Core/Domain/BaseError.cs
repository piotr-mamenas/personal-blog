using System;

namespace PersonalBlog.Web.Core.Domain
{
    public class BaseError
    {
        public virtual string RedirectAction { get; set; }
        public virtual string RedirectController { get; set; }
        public virtual Exception Exception { get; set; }
        public virtual int StatusCode { get; set; }
    }
}