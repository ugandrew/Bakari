﻿namespace Bakari.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public required string CategoryCode { get; set; }
        public required string CategoryName { get; set; }

    }
}
