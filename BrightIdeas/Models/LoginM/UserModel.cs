using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models
{
    public class User
    {
        [Key]
        public int Id {get;set;}
        [Required]
        [RegularExpression("^((?=.{1,})(?=.*[a-zA-Z])).*$", ErrorMessage = "Name must be at least 1 character and contain only letters and spaces.")]
        public string Name {get;set;}
        [Required]
        [RegularExpression("^((?=.{1,})(?=.*[0-9])(?=.*[a-zA-Z])).*$", ErrorMessage = "Alias must be at least 1 character and contain only numbers and letters.")]
        public string Alias {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        public List<Message> UserMessages {get;set;}
        public List<Like> UserLikes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
    }
}