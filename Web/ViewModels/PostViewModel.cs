using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PersonalBlog.Web.Core.Domain;

namespace PersonalBlog.Web.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [DisplayName("Post Title")]
        public string PostTitle { get; set; }

        [AllowHtml]
        public string PostBody { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime PostDate { get; set; }

        public User User { get; set; }
    }
}