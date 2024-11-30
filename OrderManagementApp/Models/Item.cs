﻿using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        //public List<OrderItem> OrderItems { get; set; }
    }
}
