using Loovi.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loovi.Test.ORM.Configurations
{
    /// <summary>
    /// Configures the TaskItem entity for the database.
    /// </summary>
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        /// <summary>
        /// Configures the TaskItem entity's properties and relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // Table name
            builder.ToTable("Tasks");

            // Primary key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>(); // Store the enum as a string in the database
        }
    }
}
