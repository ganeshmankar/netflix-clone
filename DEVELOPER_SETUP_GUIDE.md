# 🎬 Netflix Clone - Developer Setup Guide

## Prerequisites

Before starting, ensure you have:

1. ✅ **Visual Studio 2022** (Community/Professional/Enterprise)
   - Download: https://visualstudio.microsoft.com/downloads/
   - Workload: **ASP.NET and web development**
   - Workload: **.NET desktop development**

2. ✅ **SQL Server LocalDB** (comes with Visual Studio)
   - Or SQL Server Express: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

3. ✅ **.NET 9 SDK**
   - Download: https://dotnet.microsoft.com/download/dotnet/9.0

4. ✅ **Git** (to clone the repository)
   - Download: https://git-scm.com/downloads

---

## 📥 Step 1: Get the Project

### Option A: Clone from Git (if you have a repository)
```bash
git clone <your-repo-url>
cd netflix-clone
```

### Option B: Copy Project Files
1. Copy the entire `netflix-clone` folder to your machine
2. Place it somewhere like `C:\Projects\netflix-clone`

---

## 📂 Step 2: Verify Project Structure

Your project should have this structure:

```
netflix-clone/
├── src/
│   ├── NetflixClone.Domain/
│   ├── NetflixClone.Application/
│   ├── NetflixClone.Infrastructure/
│   └── NetflixClone.API/
├── NetflixClone.sln
└── README.md
```

---

## 🔧 Step 3: Open Solution in Visual Studio

1. Open **Visual Studio 2022**
2. Click **File → Open → Project/Solution**
3. Navigate to `netflix-clone` folder
4. Select **NetflixClone.sln**
5. Click **Open**

---

## 📦 Step 4: Restore NuGet Packages

Visual Studio should automatically restore packages. If not:

1. Right-click on **Solution** in Solution Explorer
2. Click **Restore NuGet Packages**
3. Wait for completion

### Manual Package Installation (if needed)

Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console)

Run these commands:

```powershell
# Domain
Install-Package MediatR.Contracts -Version 2.0.1 -Project NetflixClone.Domain

# Application
Install-Package MediatR -Version 12.4.1 -Project NetflixClone.Application
Install-Package AutoMapper -Version 13.0.1 -Project NetflixClone.Application
Install-Package FluentValidation -Version 11.10.0 -Project NetflixClone.Application
Install-Package FluentValidation.DependencyInjectionExtensions -Version 11.10.0 -Project NetflixClone.Application

# Infrastructure
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Microsoft.EntityFrameworkCore.Design -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Microsoft.Extensions.Configuration.Abstractions -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Microsoft.Extensions.Caching.Memory -Version 9.0.0 -Project NetflixClone.Infrastructure
Install-Package Hangfire.AspNetCore -Version 1.8.20 -Project NetflixClone.Infrastructure
Install-Package Hangfire.SqlServer -Version 1.8.20 -Project NetflixClone.Infrastructure
Install-Package MediatR -Version 12.4.1 -Project NetflixClone.Infrastructure
Install-Package FluentValidation.DependencyInjectionExtensions -Version 11.10.0 -Project NetflixClone.Infrastructure

# API
Install-Package Microsoft.EntityFrameworkCore.Design -Version 9.0.0 -Project NetflixClone.API
Install-Package MediatR -Version 12.4.1 -Project NetflixClone.API
Install-Package Swashbuckle.AspNetCore -Version 6.5.0 -Project NetflixClone.API
```

---

## 🏗️ Step 5: Build the Solution

1. Click **Build → Rebuild Solution**
2. Wait for build to complete
3. Check **Output** window - should say **"Build succeeded"**

**If you get errors:**
- Check that all NuGet packages are installed
- Make sure you're using **.NET 9**
- Close and reopen Visual Studio

---

## 🗄️ Step 6: Create the Database

### 6.1: Verify SQL Server LocalDB

Open **Command Prompt** and run:
```cmd
sqllocaldb info
```

You should see `mssqllocaldb` listed.

If not, install SQL Server LocalDB or use SQL Server Express.

### 6.2: Create Database Migration

In **Package Manager Console**, run:

```powershell
Add-Migration InitialCreate -Project NetflixClone.Infrastructure -StartupProject NetflixClone.API
```

**Expected output:**
```
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

### 6.3: Apply Migration (Create Database)

```powershell
Update-Database -Project NetflixClone.Infrastructure -StartupProject NetflixClone.API
```

**Expected output:**
```
Build succeeded.
Applying migration '20260216XXXXXX_InitialCreate'.
Done.
```

### 6.4: Verify Database Created

1. Open **View → SQL Server Object Explorer**
2. Expand **SQL Server → (localdb)\MSSQLLocalDB → Databases**
3. You should see **NetflixCloneDb**
4. Expand **Tables** - you should see:
   - Movies
   - Users
   - WatchHistories
   - MyListItems
   - Rooms
   - RoomParticipants
   - MovieGenres
   - AspNetUsers (and other Identity tables)
   - HangFire tables

---

## ▶️ Step 7: Run the Application

1. Set **NetflixClone.API** as startup project:
   - Right-click **NetflixClone.API** in Solution Explorer
   - Click **Set as Startup Project**

2. Press **F5** or click **▶ Start** button

3. Browser should open automatically to **Swagger UI**

**Expected URL:**
```
https://localhost:7093
```

---

## 🧪 Step 8: Test the API

### 8.1: Create Test User

In Swagger, find **POST /api/users/create-test-user**

1. Click **"Try it out"**
2. Click **"Execute"**

**Expected Response:**
```json
{
  "message": "Test user created successfully",
  "id": "00000000-0000-0000-0000-000000000001",
  "email": "test@netflixclone.com"
}
```

### 8.2: Create a Movie

Find **POST /api/movies**

1. Click **"Try it out"**
2. Use this JSON:

```json
{
  "title": "Inception",
  "description": "A thief who steals corporate secrets through dream-sharing technology",
  "releaseYear": 2010,
  "durationMinutes": 148,
  "videoUrl": "https://example.com/inception.mp4",
  "thumbnailUrl": "https://example.com/inception-thumb.jpg",
  "bannerUrl": "https://example.com/inception-banner.jpg",
  "rating": 3,
  "director": "Christopher Nolan",
  "cast": "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
  "genres": [0, 1]
}
```

3. Click **"Execute"**

**Expected Response:** `201 Created`
```json
{
  "id": "some-guid-here"
}
```

### 8.3: Get All Movies

Find **GET /api/movies**

1. Click **"Try it out"**
2. Click **"Execute"**

**Expected Response:** `200 OK` with array of movies

### 8.4: Health Check

Find **GET /api/health**

1. Click **"Try it out"**
2. Click **"Execute"**

**Expected Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2026-02-16T12:00:00Z",
  "service": "Netflix Clone API",
  "version": "1.0.0"
}
```

---

## 🎯 Step 9: Verify Hangfire Dashboard

Navigate to:
```
https://localhost:7093/hangfire
```

You should see the Hangfire dashboard with:
- Jobs
- Recurring Jobs
- Servers
- Succeeded/Failed jobs

---

## ✅ Success Checklist

- [ ] Visual Studio 2022 installed
- [ ] .NET 9 SDK installed
- [ ] SQL Server LocalDB running
- [ ] Solution opens without errors
- [ ] All NuGet packages restored
- [ ] Solution builds successfully (0 errors)
- [ ] Database migration created
- [ ] Database created (NetflixCloneDb)
- [ ] Application runs (F5)
- [ ] Swagger UI loads
- [ ] Test user created
- [ ] Can create movies
- [ ] Can get movies
- [ ] Health check returns 200
- [ ] Hangfire dashboard accessible

---

## 🐛 Common Issues & Solutions

### Issue 1: "Could not find SDK"
**Solution:** Install .NET 9 SDK from https://dotnet.microsoft.com/download/dotnet/9.0

### Issue 2: "NuGet packages not found"
**Solution:** 
1. Tools → Options → NuGet Package Manager → Package Sources
2. Ensure `nuget.org` is enabled
3. Restore packages again

### Issue 3: "Database migration fails"
**Solution:**
1. Check SQL Server LocalDB is running: `sqllocaldb start mssqllocaldb`
2. Verify connection string in `appsettings.json`
3. Try: `sqllocaldb delete mssqllocaldb` then `sqllocaldb create mssqllocaldb`

### Issue 4: "Build errors"
**Solution:**
1. Clean solution: **Build → Clean Solution**
2. Close Visual Studio
3. Delete `bin` and `obj` folders in all projects
4. Reopen and rebuild

### Issue 5: "Port already in use"
**Solution:**
1. Open `Properties/launchSettings.json` in NetflixClone.API
2. Change port numbers in `applicationUrl`

### Issue 6: "MediatR version conflicts"
**Solution:** Follow the exact package versions in Step 4

---

## 📚 Project Architecture

```
┌─────────────────────────────────────────┐
│          NetflixClone.API               │  ← Controllers, Middleware
│         (Presentation Layer)            │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│      NetflixClone.Application           │  ← CQRS, Handlers, DTOs
│        (Business Logic)                 │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│     NetflixClone.Infrastructure         │  ← EF Core, Repositories
│         (Data Access)                   │
└─────────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────────┐
│       NetflixClone.Domain               │  ← Entities, Events
│         (Core Domain)                   │
└─────────────────────────────────────────┘
```

---

## 🎓 What's Implemented

✅ **Phase 1:** Domain Layer (Entities, Events, Enums)  
✅ **Phase 2:** Application Layer (CQRS, MediatR, Handlers)  
✅ **Phase 3:** Infrastructure Layer (EF Core, Repositories, Database)  
✅ **Phase 4:** API Layer (Controllers, Middleware, Swagger)  

🔜 **Phase 5:** SignalR (Real-time watch parties)  
🔜 **Phase 6:** React Frontend  

---

## 📞 Need Help?

If you encounter issues:

1. Check the **Output** window in Visual Studio
2. Check the **Error List** window
3. Review this guide step-by-step
4. Check `appsettings.json` for correct connection string

---

## 🎉 You're All Set!

Once all tests pass, you have a fully functional Netflix Clone backend API!

**Next Steps:**
- Explore the API endpoints in Swagger
- Test all CRUD operations
- Review the code architecture
- Prepare for Phase 5 (SignalR)

---

**Happy Coding! 🚀**
