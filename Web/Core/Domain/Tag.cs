using System.Collections.Generic;

namespace PersonalBlog.Web.Core.Domain
{
    public class Tag
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}