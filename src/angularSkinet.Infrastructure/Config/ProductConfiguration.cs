using angularSkinet.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace angularSkinet.Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("nvarchar(500)");
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.PictureUrl)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar(255)");
        
        builder.Property(p => p.Type)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("nvarchar(50)");
        
        builder.Property(p => p.Brand)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("nvarchar(50)");

        builder.Property(p => p.QuantityInStock)
            .IsRequired()
            .HasDefaultValue(0);
        
        
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.Type);
        builder.HasIndex(p => p.Brand);
    }
}