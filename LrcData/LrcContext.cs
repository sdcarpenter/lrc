using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LrcData
{
    public class LrcContext : DbContext
    {
        public LrcContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lobby>()
                .HasKey(c => c.LobbyName);
        }

        public DbSet<Lobby> Lobbies { get; set; }
    }

    public class LrcContextFactory : IDesignTimeDbContextFactory<LrcContext>
    {
        public LrcContext CreateDbContext(string[] args)
        {
            // IDesignTimeDbContextFactory is used usually when you execute EF Core commands like Add-Migration, Update-Database, and so on
            // So it is usually your local development machine environment
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                          Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            // Prepare configuration builder
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LrcContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("LrcContext"));

            return new LrcContext(optionsBuilder.Options);
        }
    }
}
