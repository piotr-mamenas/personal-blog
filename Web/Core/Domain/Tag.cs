using System.Collections.Generic;

namespace PersonalBlog.Web.Core.Domain
{
    public class Tag
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}