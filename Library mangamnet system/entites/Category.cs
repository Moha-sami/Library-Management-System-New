using System;
using System.Collections.Generic;
using System.Text;

namespace Library_mangamnet_system.entites
{
    public class Category : Baseentity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
