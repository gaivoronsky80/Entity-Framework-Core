using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        [EmailAddress]
        [Required]
        public string Email {get; set;}
        [DataType(DataType.Password)]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^((?=.{8,})(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[!@#$%^&+=])).*$", ErrorMessage = "Passwords must be at least 8 characters and contain at  least 1 number, 1 letter, and a special character.")]
        public string Password {get; set;}
        public List<Join> Joins {get; set;}
        public List<Event> Events {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get; set;}
    }
}