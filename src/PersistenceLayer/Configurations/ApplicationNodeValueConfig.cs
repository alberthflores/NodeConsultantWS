using DomainLayer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersistenceLayer.Configurations
{
    public class ApplicationNodeValueConfig
    {
        public ApplicationNodeValueConfig(EntityTypeBuilder<NodeValue> entityBuilder)
        {
            entityBuilder.HasOne(nv => nv.Node)
                        .WithMany(n => n.Values)
                        .HasForeignKey(nv =>nv.NodeId);
                
        }
    }
}
