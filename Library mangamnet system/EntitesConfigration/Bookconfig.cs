using System;
using System.Collections.Generic;
using System.Text;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class Bookconfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a=>a.Price)
                .HasPrecision(6, 2);
            builder.ToTable(TableBuilder => TableBuilder.HasCheckConstraint("CK_Book_PublicationYear", "PublicationYear >= 1950 AND PublicationYear <= YEAR(GETDATE())"))
                .ToTable(TableBuilder => TableBuilder.HasCheckConstraint("CK_Book_AvailableCopies", "AvailableCopies >= 0 AND AvailableCopies <= TotalCopies"));
           

        }
    }
}
