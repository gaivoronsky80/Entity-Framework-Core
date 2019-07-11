using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyFinder.Models
{
    public class Party
    {
        [Key]
        public int PartyId {get; set;}
        [Required]
        public string Title {get; set;}
        [Required]
        public string Genre {get; set;}
        [Required]
        public string Img {get; set;}
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
        public string Location {get; set;}
        [Required]
        [MaxLength(1000, ErrorMessage="Can not be longer than 1000 characters!")]
        public string Description {get; set;}
        public List<Join> Joins {get; set;}
        public int UserId {get; set;}
        public User Creator {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
    }
}