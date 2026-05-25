using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(m => m.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(m => m.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(11);
            builder.Property(m => m.Address)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(m => m.Status)
                .HasConversion<string>()
                .HasMaxLength(9);
            builder.Property(m => m.MembershipDate)
    .IsRequired(false)
    .HasDefaultValueSql("GETDATE()");
            builder.ToTable(tablebuilder =>
            {
                tablebuilder.HasCheckConstraint("CK_Member_Email", "Email LIKE '%@%' AND Email LIKE '%.%'");
            });
            builder.ToTable(t => t.HasCheckConstraint("CK_Member_PhoneNumber",
    "PhoneNumber LIKE '01[0-2,5]%' AND LEN(PhoneNumber) = 11 AND PhoneNumber NOT LIKE '%[^0-9]%'"));

        }
    }
}
