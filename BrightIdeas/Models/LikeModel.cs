using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class Like
    {
        [Key]
        public int Id {get;set;}
        public int UserId {get;set;}
        public int MessageId {get;set;}
        public User User{get;set;}
        public Message Message{get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}