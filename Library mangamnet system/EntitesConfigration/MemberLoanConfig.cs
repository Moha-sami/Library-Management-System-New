using System;
using System.Collections.Generic;
using System.Text;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_mangamnet_system.EntitesConfigration
{
    internal class MemberLoanConfig : IEntityTypeConfiguration<Memberloan>
    {
        public void Configure(EntityTypeBuilder<Memberloan> builder)
        {
            //composite key
            builder.HasKey(ml => new { ml.MemberId, ml.LoanId,ml.BookId });
        }
    }
}
