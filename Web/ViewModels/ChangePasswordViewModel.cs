using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DisplayName("Repeat New Password")]
        public string RepeatNewPassword { get; set; }
    }
}