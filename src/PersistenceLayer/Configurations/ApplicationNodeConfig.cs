using DomainLayer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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
            #region Seeds
            entityBuilder.HasData(
                new Node { Id=1, Description="Nodo 1", ParentId = null,CreatedAt=DateTime.Now},
                new Node { Id=2, Description="Nodo 2", ParentId = 1,CreatedAt=DateTime.Now},
                new Node { Id=3, Description="Nodo 3", ParentId = 1, CreatedAt=DateTime.Now},
                new Node { Id=4, Description="Nodo 4", ParentId = 2,CreatedAt=DateTime.Now},
                new Node { Id=5, Description="Nodo 5", ParentId = 2, CreatedAt=DateTime.Now},
                new Node { Id=6, Description="Nodo 6", ParentId = 3, CreatedAt=DateTime.Now},
                new Node { Id=7, Description="Nodo 7", ParentId = 4,CreatedAt=DateTime.Now},
                new Node { Id=8, Description="Nodo 8", ParentId = 4, CreatedAt=DateTime.Now},
                new Node { Id=9, Description="Nodo 9", ParentId = 5, CreatedAt=DateTime.Now},
                new Node { Id=10, Description="Nodo 10", ParentId = 5, CreatedAt=DateTime.Now},
                new Node { Id=11, Description="Nodo 11", ParentId = 6, CreatedAt=DateTime.Now},
                new Node { Id=12, Description="Nodo 12", ParentId = 6, CreatedAt=DateTime.Now}
                );
            #endregion
        }
    }
}
