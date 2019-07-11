using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Event
    {
        [Key]
        public int EventId {get; set;}
        [Required]
        public string Title {get; set;}
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time {get; set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get; set;}
        [Required]
        public int Duration {get; set;}
        [Required]
        public string DurationType {get; set;}
        [Required]
        [MaxLength(255, ErrorMessage="Can not be longer than 255 characters!")]
        public string Description {get; set;}
        public List<Join> Joins {get; set;}
        public int UserId {get; set;}
        public User Creator {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}