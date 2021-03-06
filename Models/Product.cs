﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a product description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a product category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter product price")]
        public decimal Price { get; set; }

        public bool? InStock { get; set; }
    }
}