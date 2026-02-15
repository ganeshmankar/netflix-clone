# 🚀 VS Code Setup Guide - Netflix Clone Project

## ✅ Current Status
You've successfully cloned the project! Now let's get everything working.

---

## 📋 Step-by-Step Setup

### **Step 1: Restore NuGet Packages**

Open the **integrated terminal** in VS Code (`Ctrl + ~` or View → Terminal) and run:

```bash
dotnet restore
```

**What this does:**
- Downloads all NuGet packages for all 4 projects
- Application: MediatR, FluentValidation, AutoMapper
- Infrastructure: EF Core, Identity, Hangfire, Caching
- Restores package dependencies

**Expected output:**
```
Restore succeeded.
```

---

### **Step 2: Verify Build**

```bash
dotnet build
```

**Expected output:**
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

**If you see errors:**
- Check that all `.csproj` files are present
- Ensure NuGet packages restored correctly
- Share the error message

---

### **Step 3: Install dotnet-ef Tool (Critical!)**

This is the tool needed for database migrations.

```bash
dotnet tool install --global dotnet-ef
```

**If already installed, update it:**
```bash
dotnet tool update --global dotnet-ef
```

**Verify installation:**
```bash
dotnet ef
```

**Expected output:**
```
                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 9.0.0
```

---

### **Step 4: Check Connection String**

Verify `src/NetflixClone.API/appsettings.json` has:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NetflixCloneDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

**If using SQL Server Express instead of LocalDB:**
```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=NetflixCloneDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
```

---

### **Step 5: Create Database Migration**

From the **project root** directory:

```bash
dotnet ef migrations add InitialCreate --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API
```

**What this creates:**
- `Infrastructure/Migrations/XXXXXX_InitialCreate.cs`
- `Infrastructure/Migrations/NetflixCloneDbContextModelSnapshot.cs`

**Expected output:**
```
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

---

### **Step 6: Apply Migration (Create Database)**

```bash
dotnet ef database update --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API
```

**What this does:**
- Creates `NetflixCloneDb` database
- Creates all tables (Movies, Users, WatchHistories, etc.)
- Creates Identity tables
- Creates Hangfire tables
- Sets up indexes and foreign keys

**Expected output:**
```
Build started...
Build succeeded.
Applying migration '20260215XXXXXX_InitialCreate'.
Done.
```

---

### **Step 7: Verify Database Created**

**Option A: Using VS Code Extension**

1. Install **SQL Server (mssql)** extension in VS Code
2. Connect to `(localdb)\mssqllocaldb`
3. Expand databases → Find `NetflixCloneDb`
4. View tables

**Option B: Using Command**

```bash
dotnet ef database list --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API
```

**Option C: Using SSMS**

1. Open SQL Server Management Studio
2. Connect to: `(localdb)\mssqllocaldb`
3. Expand Databases → `NetflixCloneDb`

---

## 📦 Package Summary

### **Application Layer** (NetflixClone.Application.csproj)
```xml
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
<PackageReference Include="MediatR" Version="12.2.0" />
```

### **Infrastructure Layer** (NetflixClone.Infrastructure.csproj)
```xml
<PackageReference Include="Hangfire.AspNetCore" Version="1.8.9" />
<PackageReference Include="Hangfire.SqlServer" Version="1.8.9" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.1" />
<PackageReference Include="Microsoft.Extensions.Caching.Memory" Version="8.0.0" />
```

---

## 🔍 Verification Checklist

Run these commands to verify everything:

```bash
# 1. Check .NET SDK version
dotnet --version
# Should be 8.0 or higher

# 2. Restore packages
dotnet restore

# 3. Build solution
dotnet build

# 4. Check dotnet-ef installed
dotnet ef

# 5. List migrations (after creating)
dotnet ef migrations list --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API

# 6. Check database (after update)
dotnet ef database list --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API
```

---

## 🐛 Common Issues & Solutions

### Issue 1: "dotnet-ef not found"

**Solution:**
```bash
# Install globally
dotnet tool install --global dotnet-ef

# Add to PATH (if needed)
# Windows: Add %USERPROFILE%\.dotnet\tools to PATH
# Mac/Linux: Add ~/.dotnet/tools to PATH
```

### Issue 2: "Build failed"

**Solution:**
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Issue 3: "Unable to create DbContext"

**Solution:**
- Check `appsettings.json` exists in API project
- Verify connection string is correct
- Ensure SQL Server LocalDB is installed

### Issue 4: "Cannot connect to LocalDB"

**Solution:**
```bash
# Start LocalDB
sqllocaldb start mssqllocaldb

# Or use SQL Server Express
# Update connection string to: Server=localhost\\SQLEXPRESS;...
```

### Issue 5: Package version conflicts

**Solution:**
```bash
# Update all packages
dotnet restore --force
```

---

## 📁 Project Structure Verification

Ensure you have these folders/files:

```
netflix-clone/
├── src/
│   ├── NetflixClone.Domain/
│   │   ├── Common/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Events/
│   ├── NetflixClone.Application/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Handlers/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   └── Validators/
│   ├── NetflixClone.Infrastructure/
│   │   ├── Data/
│   │   │   ├── NetflixCloneDbContext.cs
│   │   │   └── Configurations/
│   │   ├── Repositories/
│   │   ├── Persistence/
│   │   ├── Services/
│   │   └── DependencyInjection.cs
│   └── NetflixClone.API/
│       ├── appsettings.json ← IMPORTANT!
│       └── Program.cs
├── NetflixClone.sln
└── Documentation files (.md)
```

---

## ✅ Success Indicators

You'll know setup is complete when:

1. ✅ `dotnet restore` succeeds
2. ✅ `dotnet build` succeeds (0 errors)
3. ✅ `dotnet ef` shows EF Core logo
4. ✅ Migration files created in `Infrastructure/Migrations/`
5. ✅ Database `NetflixCloneDb` exists
6. ✅ Tables visible in database (Movies, Users, etc.)

---

## 🎯 Quick Command Reference

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Install EF tool
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API

# Apply migration
dotnet ef database update --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API

# List migrations
dotnet ef migrations list --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API

# Check databases
dotnet ef database list --project src/NetflixClone.Infrastructure --startup-project src/NetflixClone.API
```

---

## 📝 What to Tell Me

After running the setup steps, let me know:

1. ✅ **"restore done"** - if `dotnet restore` succeeded
2. ✅ **"build success"** - if `dotnet build` succeeded  
3. ✅ **"ef installed"** - if `dotnet ef` works
4. ✅ **"migration created"** - if migration files generated
5. ✅ **"database ready"** - if database created successfully

**Or share any error messages you encounter!**

---

## 🚀 After Setup Complete

Once everything is working:
- Database will be ready
- We can proceed to **Phase 4: API Layer**
- Create REST API controllers
- Test with Swagger

---

**You're doing great! Let's get this working in VS Code! 💪**
