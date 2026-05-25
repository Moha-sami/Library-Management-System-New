# 📚 Library Management System

A fully-featured **Library Management System** built with **C# and EF Core**, demonstrating clean architecture, database design, data seeding, and real-world business logic using LINQ.

---

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| **C# / .NET 10** | Backend language & runtime |
| **Entity Framework Core** | ORM for database access |
| **SQL Server** | Relational database |
| **LINQ** | Data querying & manipulation |
| **JSON** | Data seeding source files |

----

## 🏗️ Project Structure

```
Library Management System/
│
├── 📁 Dbcontext/
│   ├── AppDbContext.cs          # EF Core DbContext
│   └── LibContextSeed.cs       # Data seeding logic
│
├── 📁 Entities/
│   ├── BaseEntity.cs            # Shared base entity (Id)
│   ├── Author.cs
│   ├── Book.cs
│   ├── Category.cs
│   ├── Member.cs
│   ├── Loan.cs
│   ├── Fine.cs
│   └── MemberLoan.cs
│
├── 📁 EntitiesConfiguration/    # Clean EF Core config files
│   ├── AuthorConfig.cs
│   ├── BookConfig.cs
│   ├── CategoryConfig.cs
│   ├── MemberConfig.cs
│   ├── LoanConfig.cs
│   ├── FineConfig.cs
│   └── MemberLoanConfig.cs
│
├── 📁 Enums/
│   ├── Status.cs               # Member status (Active, Suspended)
│   ├── LoanStatus.cs           # Loan status (Borrowed, Returned, Overdue)
│   └── FineStatus.cs           # Fine status (Pending, Paid)
│
├── 📁 Migrations/              # EF Core auto-generated migrations
│
├── 📁 Data/                    # JSON seed files
│   ├── Authors.json
│   ├── Categories.json
│   ├── Books.json
│   └── Members.json
│
├── DataOperations.cs           # All LINQ data manipulation methods
└── Program.cs                  # Entry point
```

---

## 🗄️ Database Schema

```
Authors ──────────┐
                  ▼
Categories ──► Books ◄── Loans ◄── Fines
                              │
Members ──────────────► MemberLoans
```

### Tables

| Table | Description |
|---|---|
| `Authors` | Book authors with name and date of birth |
| `Categories` | Book categories (Romance, Programming, etc.) |
| `Books` | Books with price, copies, FK to Author & Category |
| `Members` | Library members with contact info and status |
| `Loans` | Loan records linking members to books |
| `MemberLoans` | Junction table with due dates and return dates |
| `Fines` | Auto-generated fines for late returns |

---

## ✨ Key Features

### 🔧 Clean EF Core Architecture
Each entity has its own isolated configuration file implementing `IEntityTypeConfiguration<T>`. The DbContext loads all configs automatically using:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
```

No more 500-line `OnModelCreating` methods!

---

### 🌱 Data Seeding
Seed data is loaded from JSON files at runtime using a dedicated `LibContextSeed` class:

```csharp
var authors = LoadDataFromJson<Author>("Authors.json");
context.Authors.AddRange(authors);
context.SaveChanges();
```

Seeding is **idempotent** — it checks if data already exists before inserting, so it's safe to run multiple times.

---

### 🔍 Data Manipulation with LINQ

#### 1. Projection — Books over 300 EGP
```csharp
context.Books
    .Where(b => b.Price > 300)
    .Select(b => new {
        b.Title,
        Category = b.Category.Title,
        Author = b.Author.FirstName + " " + b.Author.LastName
    })
    .ToList();
```

#### 2. Eager Loading — Authors with their books
```csharp
context.Authors
    .Include(a => a.Books)
    .ToList();
```

#### 3. Borrow a Book
```csharp
// Creates Loan + MemberLoan, decrements AvailableCopies
var loan = new Loan { MemberId = ..., BookId = ..., Status = LoanStatus.Borrowed };
context.Loans.Add(loan);
context.SaveChanges();
book.AvailableCopies--;
```

#### 4. Return a Book + Auto Fine Calculation
```csharp
// If returned late → Fine = days late × 5 EGP
var daysLate = (int)(returnDate - memberLoan.Duedate).TotalDays;
var fine = new Fine { Amount = daysLate * 5, Status = FineStatus.Pending };
```

#### 5. Active Loans Report
```csharp
context.Members
    .Where(m => m.MemberLoans.Any(ml => ml.Returndate == null))
    .ToList();
```

---

## 🛡️ Database Constraints

| Table | Constraint | Rule |
|---|---|---|
| `Members` | `CK_Member_Email` | Must contain `@` and `.` |
| `Members` | `CK_Member_PhoneNumber` | Egyptian format `01[0,1,2,5]XXXXXXXX` |
| `Books` | `CK_Book_AvailableCopies` | `0 <= Available <= Total` |
| `Books` | `CK_Book_PublicationYear` | Between 1950 and current year |

---

## ⚙️ Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/YOUR_USERNAME/Library-Management-System.git
cd Library-Management-System
```

**2. Update the connection string in `AppDbContext.cs`**
```csharp
optionsBuilder.UseSqlServer(@"Server=.;Database=LibraryManagementSystem;
    Trusted_Connection=True;TrustServerCertificate=True;");
```

**3. Apply migrations**
```bash
dotnet ef database update
```

**4. Run the project**
```bash
dotnet run
```

The app will automatically seed the database on first run! ✅

---

## 📦 Seed Data

| Entity | Records |
|---|---|
| Authors | 6 |
| Categories | 7 |
| Books | 9 |
| Members | 15 |

---

## 📖 What I Learned

- ✅ Clean EF Core architecture using `IEntityTypeConfiguration<T>`
- ✅ Database design with proper relationships and constraints
- ✅ Data seeding from JSON files with idempotency checks
- ✅ LINQ projections, eager loading, and filtered queries
- ✅ Real-world business logic — borrowing, returning, and fines
- ✅ Handling SQL Server CHECK constraints with seed data

---

## 🙏 Acknowledgements

Built as part of a hands-on EF Core learning project. Every bug, constraint violation, and silent rollback was a lesson! 💪

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
