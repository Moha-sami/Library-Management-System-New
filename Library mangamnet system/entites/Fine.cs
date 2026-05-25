using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Fine : Baseentity
    {
       
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateOnly PaidDate { get; set; }
        public Enums.FineStatus Status { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }=null!;

    }
}
