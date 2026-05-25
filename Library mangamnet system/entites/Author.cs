

namespace Library_mangamnet_system.entites
{
    public class Author : Baseentity
    {


        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
