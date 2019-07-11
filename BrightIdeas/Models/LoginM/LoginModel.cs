using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models
{
    public class LoginUser
    {
        [Required]
        public string Email {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}