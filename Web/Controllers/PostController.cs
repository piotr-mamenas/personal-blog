using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PersonalBlog.Web.Attributes;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// Allows CRUD operations on Post and enables actions present in the Post Views
    /// </summary>
    [Authorize]
    [HandleError]
    public class PostController : BaseController
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IRepository<Post> _postRepository;

        /// <summary>
        /// DI enabled constructor
        /// </summary>
        /// <param name="postRepository"></param>
        public PostController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// The main list view from which items to be edited or deleted can be selected
        /// </summary>
        [Route("posts")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("posts/list")]
        public ActionResult List()
        {
            IEnumerable<Post> posts = _postRepository.GetAll().ToList();

            return View(Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts));
        }

        /// <summary>
        /// Displays the edit view with the item which needs to be updated
        /// </summary>
        /// <param name="editPostId">Id of the post to be displayed</param>
        [Route("posts/edit/{editPostId}")]
        public ActionResult Edit(int editPostId)
        {
            Post currentPost;

            if (editPostId != 0)
            {
                currentPost = _postRepository.GetById(editPostId);
            }
            else
            {
                currentPost = new Post
                {
                    PostDate = DateTime.Today,
                    PostId = 0,
                    PostTitle = "",
                    PostBody = ""
                };
            }

            return View(Mapper.Map<PostViewModel>(currentPost));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("posts/save")]
        public ActionResult Save(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var post = Mapper.Map<Post>(model);

            post.PostDate = DateTime.Today;

            if (post.PostId == 0)
            {
                _postRepository.Create(post);
            }
            else
            {
                _postRepository.Update(post);
            }

            return RedirectToAction("List");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("posts/edit/{editPostId}")]
        public ActionResult Edit(PostViewModel model, string action)
        {
            switch (action)
            {
                case "Save":
                    return RedirectToAction("Save", model);
                case "Back":
                    return RedirectToAction("List");
            }

            return View();
        }

        /// <summary>
        /// Method used to delete an existing post
        /// </summary>
        /// <param name="deletePostId">Id of post to be deleted</param>
        /// <returns></returns>
        [Route("posts/delete/{deletePostId}")]
        public ActionResult Delete(int deletePostId)
        {
            if (deletePostId != 0)
            {
                _postRepository.Delete(deletePostId);
            }

            return RedirectToAction("List");
        }
    }
}