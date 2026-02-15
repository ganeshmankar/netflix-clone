# 🔧 Database Migration Setup Guide

## Step 1: Install EF Core Tools ✅

The `dotnet-ef` tool is required to create and apply migrations.

**Command:**
```cmd
dotnet tool install --global dotnet-ef
```

**Expected Output:**
```
You can invoke the tool using the following command: dotnet-ef
Tool 'dotnet-ef' (version 'x.x.x') was successfully installed.
```

**If already installed:**
```
Tool 'dotnet-ef' is already installed.
```

**To update (if needed):**
```cmd
dotnet tool update --global dotnet-ef
```

---

## Step 2: Verify Installation

**Command:**
```cmd
dotnet ef
```

**Expected Output:**
```
                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools x.x.x
```

---

## Step 3: Create Initial Migration

**Command:**
```cmd
dotnet ef migrations add InitialCreate --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

**What this does:**
- Analyzes your `DbContext` and entity configurations
- Generates C# migration files in `Infrastructure/Migrations/`
- Creates `Up()` and `Down()` methods for schema changes
- Generates a snapshot of your model

**Expected Output:**
```
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

**Files Created:**
```
Infrastructure/Migrations/
├── 20260215XXXXXX_InitialCreate.cs
└── NetflixCloneDbContextModelSnapshot.cs
```

---

## Step 4: Review Migration (Optional)

Open the generated migration file to see the SQL operations:

```csharp
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // CREATE TABLE statements
        // CREATE INDEX statements
        // Foreign key constraints
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // DROP TABLE statements (for rollback)
    }
}
```

---

## Step 5: Apply Migration to Database

**Command:**
```cmd
dotnet ef database update --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

**What this does:**
- Creates the database if it doesn't exist
- Runs all pending migrations
- Creates all tables, indexes, and constraints
- Updates the `__EFMigrationsHistory` table

**Expected Output:**
```
Build started...
Build succeeded.
Applying migration '20260215XXXXXX_InitialCreate'.
Done.
```

---

## Step 6: Verify Database

### Option A: Using SQL Server Management Studio (SSMS)

1. Open SSMS
2. Connect to: `(localdb)\mssqllocaldb`
3. Expand Databases → `NetflixCloneDb`
4. View tables under `Tables` folder

### Option B: Using Azure Data Studio

1. Open Azure Data Studio
2. New Connection:
   - Server: `(localdb)\mssqllocaldb`
   - Authentication: Windows Authentication
   - Database: `NetflixCloneDb`
3. Explore tables

### Option C: Using dotnet-ef

**List databases:**
```cmd
dotnet ef database list --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

---

## 🗄️ Expected Database Schema

After migration, you'll have these tables:

### Core Application Tables:
```
Movies
├── Id (uniqueidentifier, PK)
├── Title (nvarchar(200))
├── Description (nvarchar(2000))
├── ReleaseYear (int)
├── DurationMinutes (int)
├── VideoUrl (nvarchar(500))
├── ThumbnailUrl (nvarchar(500))
├── BannerUrl (nvarchar(500))
├── Rating (int) - ContentRating enum
├── AverageRating (decimal(3,2))
├── PopularityScore (int)
├── ViewCount (bigint)
├── Director (nvarchar(200))
├── Cast (nvarchar(1000))
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
└── IsDeleted (bit)

Users
├── Id (uniqueidentifier, PK)
├── Email (nvarchar(256), Unique)
├── FirstName (nvarchar(100))
├── LastName (nvarchar(100))
├── ProfilePictureUrl (nvarchar(500))
├── SubscriptionTier (nvarchar(50))
├── LastLoginAt (datetime2)
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
└── IsDeleted (bit)

MovieGenres
├── Id (uniqueidentifier, PK)
├── MovieId (uniqueidentifier, FK → Movies)
├── Genre (int) - Genre enum
└── Composite Unique Index on (MovieId, Genre)

WatchHistories
├── Id (uniqueidentifier, PK)
├── UserId (uniqueidentifier, FK → Users)
├── MovieId (uniqueidentifier, FK → Movies)
├── LastWatchedPositionSeconds (int)
├── PercentageWatched (decimal(5,2))
├── IsCompleted (bit)
├── LastWatchedAt (datetime2)
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
├── IsDeleted (bit)
└── Composite Unique Index on (UserId, MovieId)

MyListItems
├── Id (uniqueidentifier, PK)
├── UserId (uniqueidentifier, FK → Users)
├── MovieId (uniqueidentifier, FK → Movies)
├── AddedAt (datetime2)
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
├── IsDeleted (bit)
└── Composite Unique Index on (UserId, MovieId)

Rooms
├── Id (uniqueidentifier, PK)
├── RoomCode (nvarchar(10), Unique)
├── Name (nvarchar(200))
├── HostUserId (uniqueidentifier)
├── MovieId (uniqueidentifier)
├── CurrentPositionSeconds (int)
├── IsPlaying (bit)
├── IsActive (bit)
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
└── IsDeleted (bit)

RoomParticipants
├── Id (uniqueidentifier, PK)
├── RoomId (uniqueidentifier, FK → Rooms)
├── UserId (uniqueidentifier, FK → Users)
├── JoinedAt (datetime2)
├── LeftAt (datetime2)
├── IsConnected (bit)
├── CreatedAt (datetime2)
├── UpdatedAt (datetime2)
└── IsDeleted (bit)
```

### ASP.NET Identity Tables:
```
AspNetUsers
AspNetRoles
AspNetUserRoles
AspNetUserClaims
AspNetUserLogins
AspNetUserTokens
AspNetRoleClaims
```

### Hangfire Tables:
```
HangFire.AggregatedCounter
HangFire.Counter
HangFire.Hash
HangFire.Job
HangFire.JobParameter
HangFire.JobQueue
HangFire.List
HangFire.Schema
HangFire.Server
HangFire.Set
HangFire.State
```

---

## 🔧 Common Issues & Solutions

### Issue 1: "Build failed"

**Solution:** Ensure the solution builds successfully first:
```cmd
dotnet build
```

### Issue 2: "No DbContext was found"

**Solution:** Make sure you're specifying both projects:
```cmd
--project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Issue 3: "Unable to create an object of type 'NetflixCloneDbContext'"

**Solution:** Ensure `appsettings.json` has the connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NetflixCloneDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

### Issue 4: "LocalDB is not installed"

**Solution:** Install SQL Server Express LocalDB:
- Download from: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb
- Or use SQL Server Express with a different connection string

### Issue 5: "A network-related or instance-specific error"

**Solution:** Start LocalDB:
```cmd
sqllocaldb start mssqllocaldb
```

---

## 🎯 Migration Commands Cheat Sheet

### Create Migration
```cmd
dotnet ef migrations add <MigrationName> --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Apply Migration
```cmd
dotnet ef database update --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Remove Last Migration (before applying)
```cmd
dotnet ef migrations remove --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### List Migrations
```cmd
dotnet ef migrations list --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Rollback to Specific Migration
```cmd
dotnet ef database update <MigrationName> --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Drop Database
```cmd
dotnet ef database drop --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

### Generate SQL Script (without applying)
```cmd
dotnet ef migrations script --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```

---

## ✅ Success Indicators

After successful migration, you should see:

1. **Migration files** in `Infrastructure/Migrations/`
2. **Database created** in SQL Server LocalDB
3. **Tables created** (20+ tables including Identity and Hangfire)
4. **Indexes created** on key columns
5. **Foreign keys** properly configured
6. **No errors** in the output

---

## 🚀 Next Steps After Migration

Once the database is created:

1. ✅ Verify tables in SSMS or Azure Data Studio
2. ✅ Test connection from the application
3. ✅ Proceed to Phase 4 - API Layer
4. ✅ Create API controllers
5. ✅ Test CRUD operations

---

**Current Status:** Waiting for `dotnet-ef` tool installation to complete.

**Next Command:** After installation completes, run:
```cmd
dotnet ef migrations add InitialCreate --project src\NetflixClone.Infrastructure --startup-project src\NetflixClone.API
```
