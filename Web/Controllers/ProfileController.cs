﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ProfileController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorRepository"></param>
        /// <param name="userRepository"></param>
        public ProfileController(IRepository<Author> authorRepository, IRepository<User> userRepository)
        {
            _authorRepository = authorRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("profile/save")]
        [HttpPost]
        public ActionResult SaveProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.FormInvalid);
                return View("Index",model);
            }

            var author = Mapper.Map<Author>(model);

            _authorRepository.Update(author);

            HandleResponse(PageResponseCode.Success, ValidationResponseCode.ProfileChangeSuccessful);
            return View("Index", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Author> _authorRepository;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<User> _userRepository;
    }
}