using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWM.Models;
using Microsoft.EntityFrameworkCore;

namespace EWM.Connecting
{
    public class EVMDataContext : DbContext
    {
        public DbSet<UsersModel> UsersViewModel { get; set; }

        public EVMDataContext(DbContextOptions<EVMDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WebApiDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersModel>().ToTable("users");
        }
    }
}
