using Microsoft.EntityFrameworkCore;
using Middleware.Data.Access;
using System;
using System.Collections.Generic;
using System.Text;


namespace Middleware.Data.Access
{
    public class Context : DbContext
    {

        public DbSet<Device> Devices { get; set; }

        public Context(DbContextOptions<Context> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>();
        }
    }
}
