using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class Message
    {
        [Key]
        public int Id {get;set;}
        public int UserId {get;set;}
        public User User{get;set;}
        public List<Like> MessageLikes {get;set;}
        [Required]
        [MinLength(5, ErrorMessage="Content must be 5 characters or longer!")]
        [Display(Name = "Content")]
        public string Content {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}