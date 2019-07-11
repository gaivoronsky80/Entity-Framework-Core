using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyFinder.Models
{
    public class Join
    {
        [Key]
        public int JoinId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int PartyId {get; set;}
        public Party Party {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}