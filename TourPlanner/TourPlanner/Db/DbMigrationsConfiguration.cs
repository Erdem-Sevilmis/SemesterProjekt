using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Db
{
    internal sealed class DbMigrationsConfiguration : DbMigrationsConfiguration<TourPlannerContext>
    {
        public DbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false; // Set to true if you want automatic migrations
        }

        protected override void Seed(TourPlannerContext context)
        {
            // Seed method for initializing data
        }
    }
}
