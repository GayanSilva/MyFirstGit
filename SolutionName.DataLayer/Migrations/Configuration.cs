namespace SolutionName.DataLayer.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SolutionName.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SolutionName.DataLayer.SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                so => so.CustomerName,
                 new SalesOrder { CustomerName = "Adam", PONumber = "123" },
                 new SalesOrder { CustomerName = "Brian", PONumber = "456" },
                 new SalesOrder { CustomerName = "James", PONumber = "789" }
                 );
        }
    }
}
