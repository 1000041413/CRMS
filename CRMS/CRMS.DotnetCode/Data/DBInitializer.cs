using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal sealed class DBInitializer : DbMigrationsConfiguration<Data.CRMSDbContext>
    {
        public DBInitializer()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.CRMSDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            
        }
    }
}
