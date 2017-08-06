using System;
using System.Collections.Generic;

namespace PersonalBlog.Web.Core.Domain
{
    public class Post
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Body { get; set; }

        public virtual DateTime Timestamp { get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}