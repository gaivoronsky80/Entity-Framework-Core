using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Join
    {
        [Key]
        public int JoinId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int EventId {get; set;}
        public Event Event {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}