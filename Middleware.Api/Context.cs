using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Middleware.Api
{
    public class Context<T> : DbContext where T : class
    {

        public DbSet<T> Devices { get; set; }

        public Context(DbContextOptions<Context<T>> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>();
        }
    }
}
