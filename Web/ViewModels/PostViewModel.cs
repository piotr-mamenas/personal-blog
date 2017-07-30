using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PersonalBlog.Web.Core.Domain;

namespace PersonalBlog.Web.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Post Title")]
        public string Title { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Timestamp { get; set; }

        public User User { get; set; }

        [DisplayName("Tags")]
        public string TagsString { get; set; }
    }
}