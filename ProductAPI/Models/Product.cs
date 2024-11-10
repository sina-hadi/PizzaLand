﻿using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public int CategoryId { get; set; }
        public Category? CategoryName { get; set; }
    }
}
