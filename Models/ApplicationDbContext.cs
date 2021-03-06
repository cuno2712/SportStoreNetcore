﻿using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<StackInfo> StackInfos { get; set; }
    }
}