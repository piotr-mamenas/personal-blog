using NHibernate.Type;

namespace PersonalBlog.Web.Core.Domain
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Username { get; set; }

        public virtual string Email { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual YesNoType IsRememberPasswordChecked { get; set; }

        public virtual Author Author { get; set; }

        public virtual Post Post { get; set; }
    }
}