using System;
using System.Collections.Generic;
using System.Text;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_mangamnet_system.EntitesConfigration
{
    public class LoanConfig: IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.Property(l => l.LoanDate)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(l => l.Status)
                .HasConversion<string>()
                .HasMaxLength(8);

            builder.HasOne(l => l.Fine)
           .WithOne(f => f.Loan)
           .HasForeignKey<Fine>(f => f.LoanId);

        }
    }
}
