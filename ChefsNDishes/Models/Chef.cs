using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId{get; set;}
        [Required]
		[MinLength(2)]
		[Display(Name = "First Name")]
        public string FirstName{get; set;}
        [Required]
		[MinLength(2)]
		[Display(Name = "Last Name")]
        public string LastName{get; set;}
        [Required]
		[DataType(DataType.Date)]
		[Display(Name = "Date of Birth")]
        public DateTime DateOfBirth{get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Dish> DishOfChef{get; set;}
    }
}