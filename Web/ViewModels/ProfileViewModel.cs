using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PersonalBlog.Web.Core.Domain;

namespace PersonalBlog.Web.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        public User User { get; set; }

        [AllowHtml]
        [DisplayName("About Me")]
        public string AuthorDescription { get; set; }
    }
}