using Microsoft.EntityFrameworkCore;
using Loovi.Test.Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loovi.Test.ORM
{
    /// <summary>
    /// Represents the main database context for the application.
    /// </summary>
    public class MainContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainContext"/> class.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for tasks.
        /// </summary>
        public DbSet<TaskItem> Tasks { get; set; }

        /// <summary>
        /// Configures the model and relationships for the database.
        /// </summary>
        /// <param name="modelBuilder">The model builder to configure the database schema.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Factory class for creating instances of <see cref="MainContext"/> at design time.
        /// </summary>
        public class DbContextFactory : IDesignTimeDbContextFactory<MainContext>
        {
            public MainContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<MainContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                builder.UseSqlServer(
                       connectionString,
                       b => b.MigrationsAssembly("Loovi.Test.ORM")
                );

                return new MainContext(builder.Options);
            }
        }
    }
}
