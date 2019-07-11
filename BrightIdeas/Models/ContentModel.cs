using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class FormContent
    {
        [Required]
        [MinLength(5, ErrorMessage="Content must be 5 characters or longer!")]
		[Display(Name = "Content")]
        public string Content {get;set;}
    }
}