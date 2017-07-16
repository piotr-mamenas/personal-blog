using System;

namespace PersonalBlog.Web.Core.Domain
{
    public class Post
    {
        public virtual int PostId { get; set; }

        public virtual string PostTitle { get; set; }

        public virtual string PostBody { get; set; }

        public virtual DateTime PostDate { get; set; }

        public virtual User User { get; set; }
    }
}