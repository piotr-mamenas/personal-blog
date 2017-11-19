using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Enums;
using PersonalBlog.Web.ViewModels;

namespace PersonalBlog.Web.Controllers
{
    /// <summary>
    /// Allows CRUD operations on Post and enables actions present in the Post Views
    /// </summary>
    [Authorize]
    public class PostController : BaseController
    {
        /// <summary>
        /// DI enabled constructor
        /// </summary>
        /// <param name="postRepository"></param>
        /// <param name="tagRepository"></param>
        public PostController(IRepository<Post> postRepository, IRepository<Tag> tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
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
            var post = _postRepository.GetById(editPostId);

            if (editPostId != 0)
            {
                var postTags = _tagRepository.GetAll().Where(t => t.Posts.Contains(post));
                var tagString = new StringBuilder();

                foreach (var tag in postTags)
                {
                    tagString.Append(" #" + tag.Name);
                }

                var model = Mapper.Map<PostViewModel>(post);
                model.TagsString = tagString.ToString();
                return View(model);
            }
            return View(new PostViewModel());
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
                    {
                        if (!ModelState.IsValid)
                        {
                            HandleResponse(PageResponseCode.Error, ValidationResponseCode.FormInvalid);
                            return View("Edit", model);
                        }

                        var post = Mapper.Map<Post>(model);
                        post.Timestamp = DateTime.Now;
                        post.Tags = GetTagsFromTagsString(model.TagsString, true);

                        if (model.Id == 0)
                        {
                            _postRepository.Create(post);
                        }
                        else
                        {
                            _postRepository.Update(post);
                        }

                        return RedirectToAction("List");
                    }
                case "Back":
                    {
                        return RedirectToAction("List");
                    }

                default:
                {
                    return new HttpNotFoundResult();
                }
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagsString"></param>
        /// <param name="createTagIfNotFound"></param>
        /// <returns></returns>
        private IEnumerable<Tag> GetTagsFromTagsString(string tagsString, bool createTagIfNotFound)
        {
            var tagList = new List<Tag>();

            if (tagsString == null)
            {
                HandleResponse(PageResponseCode.Error,ValidationResponseCode.FormInvalid);
                return null;
            }

            var regex = new Regex(@"(?<=#)\w+");
            
            var hashtags = regex.Matches(tagsString);

            foreach (Match hashtag in hashtags)
            {
                var selectedTag = _tagRepository.GetAll().SingleOrDefault(t => t.Name == hashtag.Value);

                if (selectedTag != null)
                {
                    tagList.Add(selectedTag);
                }
                else
                {
                    if (createTagIfNotFound)
                    {
                        selectedTag = new Tag
                        {
                            Description = "",
                            Name = hashtag.Value
                        };

                        _tagRepository.Create(selectedTag);

                        tagList.Add(selectedTag);
                    }
                }
            }
            return tagList;
        }

        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Tag> _tagRepository;
    }
}