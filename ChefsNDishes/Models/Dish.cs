using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId{get; set;}
        [Required]
		[MinLength(2)]
        public string Name{get; set;}
        [Required]
		[Range(0,9999)]
        public int Calories{get; set;}
        [Required]
		[Range(1,5)]
        public int Tastiness{get; set;}
        [Required]
		[MinLength(2)]
        public string Description{get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        [Required]
		[Display(Name = "Chef")]
        public int ChefId{get; set;}
        public Chef Chef{get; set;}
    }
}