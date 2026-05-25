using System;
using System.Collections.Generic;
using System.Text;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class FineConfig : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.Property(f => f.Amount)
                .HasPrecision(6, 2);
            builder.Property(f => f.IssuedDate)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(f => f.Status)
                .HasConversion<string>()
                .HasMaxLength(7);
        }
    
    }
}
