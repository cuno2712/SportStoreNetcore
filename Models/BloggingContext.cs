using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class BloggingContext
    {
        public DbSet<User> Users { get; set; }

        public BloggingContext(DbContextOptions<BloggingContext> options)
: base(options) { }

    }
}
