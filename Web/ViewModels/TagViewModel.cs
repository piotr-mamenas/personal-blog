﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Web.ViewModels
{
    public class TagViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("#")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}