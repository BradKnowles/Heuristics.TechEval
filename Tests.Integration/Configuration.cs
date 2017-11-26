using System.Data.Entity.Migrations;

namespace Tests.Integration
{
    internal sealed class Configuration : DbMigrationsConfiguration<TestDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = "..\\Core\\Migrations";
        }
    }
}
