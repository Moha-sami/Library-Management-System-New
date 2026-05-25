using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c=>c.Title)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(c=>c.Description)
                .HasColumnType("varchar")
                .HasMaxLength (100);
        }
    }
}
