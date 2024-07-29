using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bakari.Models;

namespace Bakari.Data
{
    public class BakariContext : DbContext
    {
        public BakariContext (DbContextOptions<BakariContext> options)
            : base(options)
        {
        }

        public DbSet<Bakari.Models.Category> Category { get; set; } = default!;
        public DbSet<Bakari.Models.Item> Item { get; set; } = default!;
        public DbSet<Bakari.Models.Stock> Stock { get; set; } = default!;
        public DbSet<Bakari.Models.Basket> Basket { get; set; } = default!;
        public DbSet<Bakari.Models.Order> Order { get; set; } = default!;
        public DbSet<Bakari.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<Bakari.Models.Transanction> Transanction { get; set; } = default!;
    }
}
