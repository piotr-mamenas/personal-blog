using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using PersonalBlog.Web.Attributes;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    [NoCache]
    [Authorize]
    [HandleError]
    public class ProfileController : BaseController
    {
        public ProfileController(IRepository<Author> authorRepository, IRepository<User> userRepository)
        {
            _authorRepository = authorRepository;
            _userRepository = userRepository;
        }

        [Route("profile/save")]
        [HttpPost]
        public ActionResult SaveProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = ReturnCode.Error;
                ViewBag.Error = "Form is not valid. Please review and try again.";
                return View("Index",model);
            }

            var author = Mapper.Map<Author>(model);

            _authorRepository.Update(author);

            ViewBag.Result = ReturnCode.Success;
            return View("Index", model);
        }

        [Route("profile/edit")]
        public ActionResult Index(ProfileViewModel model)
        {
            return View();
        }

        [Route("profile")]
        public ActionResult Index()
        {
            var authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                ViewBag.Result = ReturnCode.Error;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);

            if (ticket == null)
            {
                return View();
            }

            var username = ticket.Name;
            var user = _userRepository.GetAll().SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                return View();
            }

            var linkedAuthor = _authorRepository.GetAll().SingleOrDefault(x => x.User.Id == user.Id);

            var newAuthor = new Author
            {
                FirstName = "",
                LastName = "",
                AuthorDescription = "",
                User = user
            };

            // If there is no profile for that user just create a new one
            if (linkedAuthor == null)
            {
                _authorRepository.Create(newAuthor);
                return View(Mapper.Map<ProfileViewModel>(newAuthor));
            }

            return View(Mapper.Map<ProfileViewModel>(linkedAuthor));
        }

        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<User> _userRepository;
    }
}