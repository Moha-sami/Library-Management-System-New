using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_mangamnet_system.Dbcontext
{
    public class AppDbcontext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseSqlServer(@"Server=.;Database=LibraryManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Memberloan> Memberloans { get; set; }
        public DbSet<Category> categories { get; set; }
        
    }
}
