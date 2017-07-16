using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;

namespace PersonalBlog.Web.ViewModels
{
    public class AppSettingsViewModel
    {
        [Required]
        [DisplayName("Application Name")]
        public string AppName { get; set; }

        [Required]
        [DisplayName("Connection String")]
        public string ConnectionString { get; set; }

        [Required]
        [DisplayName("Website Background Image")]
        public HttpPostedFileBase BackgroundImage { get; set; }
    }
}