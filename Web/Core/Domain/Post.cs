using System;
using System.Collections.Generic;

namespace PersonalBlog.Web.Core.Domain
{
    public class Post
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}