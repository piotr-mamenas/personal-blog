namespace PersonalBlog.Web.Core.Domain
{
    public class Author
    {
        public virtual int Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string AuthorDescription { get; set; }

        public virtual User User { get; set; }
    }
}