using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyFinder.Models
{
    public class LoginUser
    {
        public string Email {get; set;}
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}