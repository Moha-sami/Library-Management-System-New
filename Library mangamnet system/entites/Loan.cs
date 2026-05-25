using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Loan: Baseentity
    {
        
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateOnly LoanDate { get; set; }
   
        public Enums.Loanstatus Status { get; set; }

        public int FineId { get; set; }
        public Fine? Fine { get; set; }

    }
}
