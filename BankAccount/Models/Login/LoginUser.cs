using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string Email {get; set;}
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
