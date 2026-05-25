using Library_mangamnet_system;
using Library_mangamnet_system.Dbcontext;
using Microsoft.EntityFrameworkCore;

using (var context = new AppDbcontext())
{
    bool isSeeded = LibContextSeed.SeedData(context);

    if (isSeeded)
    {
        Console.WriteLine("تم عمل Seeding للداتا بنجاح!");
    }
    else
    {
        Console.WriteLine("الداتا كانت موجودة مسبقاً أو حدث خطأ.");
    }
    // Data Operations
    DataOperations.GetExpensiveBooks(context);
    DataOperations.GetAuthorsWithBooks(context);
    DataOperations.BorrowBook(context);
    DataOperations.ReturnBook(context);
    DataOperations.GetMembersWithActiveLoans(context);

}

Console.WriteLine("اضغط Enter للخروج...");
Console.ReadLine();

