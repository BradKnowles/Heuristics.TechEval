using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Heuristics.TechEval.Core.Models;

namespace Tests.Integration
{
    internal class TestDataContext : DbContext
    {
        public TestDataContext() : base("Database")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TestDataContext>());
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
