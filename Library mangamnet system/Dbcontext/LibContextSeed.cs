using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Library_mangamnet_system.entites;
using Microsoft.EntityFrameworkCore;

namespace Library_mangamnet_system.Dbcontext
{
    public static class LibContextSeed
    {
        public static bool SeedData(AppDbcontext Dbcontext)
        {
            try
            {
                Dbcontext.Database.Migrate();

                if (Dbcontext.Authors.Any() || Dbcontext.categories.Any() ||
                    Dbcontext.Books.Any() || Dbcontext.Members.Any())
                {
                    Console.WriteLine("الداتا موجودة مسبقاً، مش هنعمل Seeding.");
                    return false;
                }

                // 1. تحميل الـ Authors
                var AuthorData = LoadDataFromJason<Author>("Authors.json");
                if (AuthorData != null) Dbcontext.Authors.AddRange(AuthorData);

                // 2. تحميل الـ Categories
                var CategoryData = LoadDataFromJason<Category>("Categories.json");
                if (CategoryData != null) Dbcontext.categories.AddRange(CategoryData);

                Console.WriteLine("جاري حفظ Authors و Categories...");
                Dbcontext.SaveChanges();
                Console.WriteLine($"✓ Authors saved: {Dbcontext.Authors.Count()}");
                Console.WriteLine($"✓ Categories saved: {Dbcontext.categories.Count()}");

                // 3. تحميل الـ Books والـ Members
                var BookData = LoadDataFromJason<Book>("Books.json");
                if (BookData != null) Dbcontext.Books.AddRange(BookData);

                var MemberData = LoadDataFromJason<Member>("Members.json");
                if (MemberData != null) Dbcontext.Members.AddRange(MemberData);

                Console.WriteLine("جاري حفظ Books و Members...");
                Dbcontext.SaveChanges();
                Console.WriteLine($"✓ Books saved: {Dbcontext.Books.Count()}");
                Console.WriteLine($"✓ Members saved: {Dbcontext.Members.Count()}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"❌ Inner Exception: {ex.InnerException?.Message}");
                Console.WriteLine($"❌ Inner Inner Exception: {ex.InnerException?.InnerException?.Message}");
                Console.WriteLine($"❌ Stack Trace: {ex.StackTrace}");
                Console.ReadLine();
                return false;
            }
        }

        private static List<T> LoadDataFromJason<T>(string fileName)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Data", fileName);

            Console.WriteLine($"--- جاري محاولة تحميل: {filePath} ---");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[خطأ]: الملف غير موجود في المسار: {filePath}");
                return new List<T>();
            }

            var jsonData = File.ReadAllText(filePath);
            Console.WriteLine($"[نجاح]: تم قراءة {jsonData.Length} حرف من الملف.");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());

            var result = JsonSerializer.Deserialize<List<T>>(jsonData, options);

            if (result == null)
            {
                Console.WriteLine($"[تحذير]: الـ Deserialization رجع null لملف {fileName}!");
                return new List<T>();
            }

            Console.WriteLine($"[نجاح]: تم تحميل {result.Count} عنصر من {fileName}.");
            return result;
        }
    }
}