using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId{get; set;}
        [Required]
		[MinLength(2)]
		[Display(Name = "Name")]
        public string Name{get; set;}
        [Required]
		[MinLength(2)]
		[Display(Name = "Description")]
        public string Description{get; set;}
        [Required]
		[Display(Name = "Price")]
        public decimal Price{get; set;}
        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
        public List<Association> Categories{get; set;}

    }
}