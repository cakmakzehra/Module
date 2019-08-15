using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace demandModul.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Demand> Demands { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Fault> Faults { get; set; }

        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }


        public DbSet<FaultType> FaultTypes { get; set; }

        public DbSet<SuggestionType> SuggestionTypes { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<UnitType> UnitTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DatabaseContext()
        {
            Database.Connection.ConnectionString = "Server=.\\SQLEXPRESS; INITIAL Catalog=ModulDB; Integrated Security=true;";
        }
    }
}