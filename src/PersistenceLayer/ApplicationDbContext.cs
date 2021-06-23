using PersistenceLayer.Configurations;
using Microsoft.EntityFrameworkCore;
using DomainLayer; 

namespace PersistenceLayer
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelCreating(modelBuilder);
        }

        private void ModelCreating(ModelBuilder modelBuilder)
        {
            new ApplicationNodeConfig(modelBuilder.Entity<Node>());
            new ApplicationNodeValueConfig(modelBuilder.Entity<NodeValue>());
        }

        public DbSet<Node> Node { get; set; }
        public DbSet<NodeValue> NodeValue { get; set; }

    }
}
