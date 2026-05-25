using Library_mangamnet_system.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Member : Baseentity
    {
        
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly? MembershipDate { get; set; }
        public Enums.Status Status { get; set; }

        public ICollection<Memberloan> Memberloans { get; set; } = new List<Memberloan>();
    }
}
