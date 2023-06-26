//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TourPlanner.Models;
using System.Data.Entity;

namespace TourPlanner.Db
{
    public class TourPlannerContext : System.Data.Entity.DbContext
    {

        public TourPlannerContext() : base("ConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure your entity mappings and relationships here
            // For example:
            // modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            // modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired();
            // modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithRequired(o => o.Customer).HasForeignKey(o => o.CustomerId);
        }
    }
}
