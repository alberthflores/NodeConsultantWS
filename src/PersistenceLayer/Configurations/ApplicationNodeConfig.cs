using DomainLayer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistenceLayer.Configurations
{
    public class ApplicationNodeConfig
    {
        public ApplicationNodeConfig(EntityTypeBuilder<Node> entityBuilder)
        {
            entityBuilder.Property(x => x.Description).HasMaxLength(20).IsRequired();
            entityBuilder.HasOne(n => n.Parent)
                .WithMany(n => n.Children)
                .HasForeignKey(n => n.ParentId);
        }
    }
}
