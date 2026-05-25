using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Memberloan : Baseentity
    {
        public DateTime Duedate { get; set; }
        public DateTime? Returndate { get; set; }

        public int BookId { get; set; }
        public Book book { get; set; }= null!;

        public int MemberId { get; set; }
        public Member member { get; set; } = null!;

        public int LoanId { get; set; }
        public Loan loan { get; set; } = null!;





    }
}
