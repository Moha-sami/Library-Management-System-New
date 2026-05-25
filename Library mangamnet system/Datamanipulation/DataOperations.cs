using System;
using System.Collections.Generic;
using System.Linq;
using Library_mangamnet_system.entites;
using Library_mangamnet_system.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace Library_mangamnet_system
{
    public static class DataOperations
    {
        // 1. Retrieve books with price > 300
        public static void GetExpensiveBooks(AppDbcontext context)
        {
            var expensiveBooks = context.Books
                .Where(b => b.Price > 300)
                .Select(b => new
                {
                    BookTitle = b.Title,
                    CategoryTitle = b.Category.Title,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName,
                    Price = b.Price
                })
                .ToList();

            Console.WriteLine("\n📚 Books with more than 300 LE:");
            Console.WriteLine("================================");
            foreach (var book in expensiveBooks)
            {
                Console.WriteLine($"Title: {book.BookTitle}");
                Console.WriteLine($"Category: {book.CategoryTitle}");
                Console.WriteLine($"Author: {book.AuthorFullName}");
                Console.WriteLine($"Price: {book.Price}");
                Console.WriteLine("--------------------------------");
            }
        }

        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        
        // 2. Retrieve all authors and their books
        public static void GetAuthorsWithBooks(AppDbcontext context)
        {
            var authorsWithBooks = context.Authors
                .Include(a => a.Books)
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.Books.Select(b => b.Title).ToList()
                })
                .ToList();

            Console.WriteLine("\n--- All Authors and Their Books ---");
            Console.WriteLine("===================================");
            foreach (var author in authorsWithBooks)
            {
                Console.WriteLine($"Author: {author.AuthorName}");
                if (author.Books.Any())
                {
                    foreach (var book in author.Books)
                        Console.WriteLine($"   -> {book}");
                }
                else
                {
                    Console.WriteLine("   -> No books yet");
                }
                Console.WriteLine("-----------------------------------");
            }
        }
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        // 3. Member 1 borrows book 2 for 5 days
        public static void BorrowBook(AppDbcontext context)
        {
            var book = context.Books.FirstOrDefault(b => b.Title == "Cairo Hearts");
            var member = context.Members.FirstOrDefault(m => m.Name == "Ali Hassan");

            if (book == null || member == null)
            {
                Console.WriteLine("Member or Book not found!");
                return;
            }

            if (book.AvailableCopies <= 0)
            {
                Console.WriteLine("Book is not available!");
                return;
            }

            var loan = new Loan
            {
                MemberId = member.Id,
                BookId = book.Id,
                LoanDate = DateOnly.FromDateTime(DateTime.Now),
                Status = Enums.Loanstatus.Borrowed,
                FineId = 0
            };
            context.Loans.Add(loan);
            context.SaveChanges();

            var memberLoan = new Memberloan
            {
                MemberId = member.Id,
                BookId = book.Id,
                LoanId = loan.Id,
                Duedate = DateTime.Now.AddDays(5),
                Returndate = null
            };
            context.Memberloans.Add(memberLoan);

            book.AvailableCopies--;

            context.SaveChanges();

            Console.WriteLine("\n--- Borrow Book ---");
            Console.WriteLine("===================");
            Console.WriteLine($"Member  : {member.Name}");
            Console.WriteLine($"Book    : {book.Title}");
            Console.WriteLine($"Borrowed: {DateTime.Now:yyyy-MM-dd}");
            Console.WriteLine($"Due Date: {DateTime.Now.AddDays(5):yyyy-MM-dd}");
            Console.WriteLine("===================");
        }
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        // 4. Member 1 returns book after 10 days
        public static void ReturnBook(AppDbcontext context)
        {
            var returnDate = DateTime.Now.AddDays(10);

            var memberLoan = context.Memberloans
                .FirstOrDefault(ml => ml.member.Name == "Ali Hassan"
                                   && ml.book.Title == "Cairo Hearts"
                                   && ml.Returndate == null);

            if (memberLoan == null)
            {
                Console.WriteLine("No active loan found!");
                return;
            }

            memberLoan.Returndate = returnDate;

            var loan = context.Loans.FirstOrDefault(l => l.Id == memberLoan.LoanId);
            if (loan != null) loan.Status = Enums.Loanstatus.Returned;

            var book = context.Books.FirstOrDefault(b => b.Title == "Cairo Hearts");
            if (book != null) book.AvailableCopies++;

            context.SaveChanges();

            Console.WriteLine("\n--- Return Book ---");
            Console.WriteLine("===================");
            Console.WriteLine($"Member     : Ali Hassan");
            Console.WriteLine($"Book       : Cairo Hearts");
            Console.WriteLine($"Due Date   : {memberLoan.Duedate:yyyy-MM-dd}");
            Console.WriteLine($"Return Date: {returnDate:yyyy-MM-dd}");

            if (returnDate > memberLoan.Duedate)
            {
                var daysLate = (int)(returnDate - memberLoan.Duedate).TotalDays;
                var fineAmount = daysLate * 5;

                var fine = new Fine
                {
                    Amount = fineAmount,
                    IssuedDate = DateTime.Now,
                    LoanId = memberLoan.LoanId,
                    Status = Enums.FineStatus.Pending,
                    PaidDate = new DateOnly()
                };
                context.Fines.Add(fine);
                context.SaveChanges();

                Console.WriteLine($"Days Late  : {daysLate} days");
                Console.WriteLine($"Fine       : {fineAmount} EGP");
            }
            else
            {
                Console.WriteLine("Returned on time!");
            }
            Console.WriteLine("===================");
        }
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        //-----------------------------------------------------------------------//
        // 5. Retrieve members with active loans
        public static void GetMembersWithActiveLoans(AppDbcontext context)
        {
            var membersWithActiveLoans = context.Members
                .Where(m => m.Memberloans.Any(ml => ml.Returndate == null))
                .Select(m => new
                {
                    MemberName = m.Name,
                    ActiveBooks = m.Memberloans
                        .Where(ml => ml.Returndate == null)
                        .Select(ml => new
                        {
                            BookTitle = ml.book.Title,
                            DueDate = ml.Duedate
                        })
                        .ToList()
                })
                .ToList();

            Console.WriteLine("\n--- Members With Active Loans ---");
            Console.WriteLine("=================================");

            if (!membersWithActiveLoans.Any())
            {
                Console.WriteLine("No members with active loans!");
            }
            else
            {
                foreach (var member in membersWithActiveLoans)
                {
                    Console.WriteLine($"Member: {member.MemberName}");
                    foreach (var book in member.ActiveBooks)
                    {
                        Console.WriteLine($"   -> {book.BookTitle} (Due: {book.DueDate:yyyy-MM-dd})");
                    }
                    Console.WriteLine("---------------------------------");
                }
            }
        }
    }
}