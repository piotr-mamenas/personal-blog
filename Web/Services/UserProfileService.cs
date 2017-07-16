using System.Web;
using System.Web.Security;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Interfaces;

namespace PersonalBlog.Web.Services
{
    public class UserProfileService : IUserProfileService
    {
        public UserProfileService(IRepository<User> userRepository, IRepository<Author> authorRepository,
            HttpContext context)
        {
            _userRepository = userRepository;
            _authorRepository = authorRepository;
            _context = context;
        }

        public string GetAuthorizedUserUsername()
        {
            var authCookie = _context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                return "";
            }

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);

            if (ticket == null)
            {
                return "";
            }

            return ticket.Name;
        }

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly HttpContext _context;
    }
}