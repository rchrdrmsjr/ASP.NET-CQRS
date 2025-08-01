using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace todoApi
{
    public class TodoDb : DbContext
    {
        private readonly ILogger<TodoDb>? _logger; // Make nullable with ?
        
        public TodoDb(DbContextOptions<TodoDb> options, ILogger<TodoDb>? logger = null)
            : base(options)
        {
            _logger = logger; // Now it's safe
        }

        public DbSet<Todo> Todos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("Todos");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.IsComplete).HasColumnName("isComplete");
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(b =>
                    b.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null)
                    .CommandTimeout(120));
                
                // Add logging if logger is available
                if (_logger != null)
                {
                    optionsBuilder.LogTo(message => _logger.LogInformation(message))
                                 .EnableSensitiveDataLogging(false);
                }
            }
        }
    }
}