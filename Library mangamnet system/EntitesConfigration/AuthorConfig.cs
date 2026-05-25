using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(20);
            builder.Property(a => a.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(20);
            
                
        }
    }
}
