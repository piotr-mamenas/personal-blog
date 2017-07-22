using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PersonalBlog.Web.Attributes;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [NoCache]
    [Authorize]
    public class TagController : BaseController
    {
        public TagController(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("tags")]
        public ActionResult Index()
        {
            var model = _tagRepository.GetAll().ToList();

            return View(Mapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("tags/create")]
        public ActionResult Create(TagViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                HandleResponse(PageResponseCode.Error, ValidationResponseCode.FormInvalid);
                return RedirectToAction("Index");
            }
            
            _tagRepository.Create(Mapper.Map<Tag>(viewModel));

            HandleResponse(PageResponseCode.Success, ValidationResponseCode.ProfileChangeSuccessful);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("tags/delete/{deleteTagId}")]
        public ActionResult Delete(int deleteTagId)
        {
            _tagRepository.Delete(deleteTagId);
            
            return RedirectToAction("Index");
        }

        private readonly IRepository<Tag> _tagRepository;
    }
}