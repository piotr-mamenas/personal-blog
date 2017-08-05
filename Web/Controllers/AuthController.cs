using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.Helpers;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    public class AuthController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public AuthController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("auth")]
        public ActionResult Index()
        {
            return View("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("auth/login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.FormInvalid);
                return View("Login");
            }

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Username == model.Username);

            if (user == null)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
                return View("Login");
            }

            if (SecurePasswordHasher.Verify(model.Password,user.PasswordHash))
                FormsAuthentication.RedirectFromLoginPage(model.Username, true);

            HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
            return View("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("auth/logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("auth/changepwd")]
        public ActionResult ChangePassword()
        {
            var authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);

            if (ticket == null)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
                return View();
            }

            var username = ticket.Name;

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
                return View();
            }

            return View(new ChangePasswordViewModel() {UserId = user.Id});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("auth/changepwd")]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.FormInvalid);
                return View();
            }

            var user = _userRepository.GetAll().SingleOrDefault(x => x.Id == model.UserId);
            
            // Correct Password Entered
            if (user != null && SecurePasswordHasher.Verify(model.OldPassword, user.PasswordHash))
            {
                if (model.NewPassword == model.RepeatNewPassword)
                {
                    user.PasswordHash = SecurePasswordHasher.Hash(model.NewPassword);
                    _userRepository.Update(user);
                    HandleResponse(PageResponseCode.Success, ValidationResponseCode.PasswordChangeSuccessful);
                    return View();
                }
            }
            HandleResponse(PageResponseCode.Error, ValidationResponseCode.CredentialsInvalid);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<User> _userRepository;
    }
}