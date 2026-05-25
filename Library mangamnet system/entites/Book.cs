using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Book : Baseentity
    {
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        // Navigation properties author
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        // Navigation properties category
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
       

        

    }
}
