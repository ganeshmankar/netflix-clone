# 🎨 Netflix Clone - Visual Architecture Guide

## 🏗️ Clean Architecture Layers (Onion Architecture)

```
╔═══════════════════════════════════════════════════════════════════════╗
║                         NetflixClone.API                              ║
║                    (Controllers, Hubs, Middleware)                    ║
║                                                                       ║
║  ┌─────────────────────────────────────────────────────────────────┐ ║
║  │              NetflixClone.Infrastructure                        │ ║
║  │   (EF Core, Repositories, SignalR, Hangfire, Caching, Blob)    │ ║
║  │                                                                 │ ║
║  │  ┌───────────────────────────────────────────────────────────┐ │ ║
║  │  │          NetflixClone.Application                         │ │ ║
║  │  │    (MediatR, Commands, Queries, DTOs, Interfaces)        │ │ ║
║  │  │                                                           │ │ ║
║  │  │  ┌─────────────────────────────────────────────────────┐ │ │ ║
║  │  │  │       NetflixClone.Domain                           │ │ │ ║
║  │  │  │   (Entities, Events, Enums)                        │ │ │ ║
║  │  │  │   ✅ COMPLETE - NO DEPENDENCIES                    │ │ │ ║
║  │  │  └─────────────────────────────────────────────────────┘ │ │ ║
║  │  │                                                           │ │ ║
║  │  └───────────────────────────────────────────────────────────┘ │ ║
║  │                                                                 │ ║
║  └─────────────────────────────────────────────────────────────────┘ ║
║                                                                       ║
╚═══════════════════════════════════════════════════════════════════════╝

Legend:
  ═══  Outer layers (depend on inner layers)
  ───  Inner layers (no outward dependencies)
  ✅   Completed
```

---

## 📊 Entity Relationship Diagram

```
┌──────────────────────────────────────────────────────────────────────┐
│                         DOMAIN ENTITIES                              │
└──────────────────────────────────────────────────────────────────────┘

                              ┌─────────────┐
                              │    User     │
                              ├─────────────┤
                              │ Id          │
                              │ Email       │
                              │ FirstName   │
                              │ LastName    │
                              └──────┬──────┘
                                     │
                    ┌────────────────┼────────────────┐
                    │                │                │
                    ▼                ▼                ▼
            ┌──────────────┐ ┌──────────────┐ ┌──────────────┐
            │WatchHistory  │ │  MyListItem  │ │RoomParticipant│
            ├──────────────┤ ├──────────────┤ ├──────────────┤
            │ UserId       │ │ UserId       │ │ UserId       │
            │ MovieId      │ │ MovieId      │ │ RoomId       │
            │ Position     │ │ AddedAt      │ │ JoinedAt     │
            │ Percentage   │ └──────┬───────┘ └──────┬───────┘
            └──────┬───────┘        │                │
                   │                │                │
                   │                │                ▼
                   │                │         ┌──────────────┐
                   │                │         │    Room      │
                   │                │         ├──────────────┤
                   │                │         │ RoomCode     │
                   │                │         │ MovieId      │
                   │                │         │ IsPlaying    │
                   │                │         │ Position     │
                   │                │         └──────┬───────┘
                   │                │                │
                   └────────────────┴────────────────┘
                                    │
                                    ▼
                            ┌──────────────┐
                            │    Movie     │
                            ├──────────────┤
                            │ Id           │
                            │ Title        │
                            │ VideoUrl     │
                            │ ThumbnailUrl │
                            │ Rating       │
                            │ Popularity   │
                            └──────┬───────┘
                                   │
                                   ▼
                            ┌──────────────┐
                            │ MovieGenre   │
                            ├──────────────┤
                            │ MovieId      │
                            │ Genre (enum) │
                            └──────────────┘
```

---

## 🔄 Request Flow (Future - When Complete)

```
┌─────────────────────────────────────────────────────────────────────┐
│                         USER REQUEST                                │
└─────────────────────────────────────────────────────────────────────┘
                                 │
                                 ▼
                    ┌────────────────────────┐
                    │   API Controller       │  ← JWT Authentication
                    │   (MoviesController)   │
                    └───────────┬────────────┘
                                │
                                │ Send Command/Query
                                ▼
                    ┌────────────────────────┐
                    │   MediatR Handler      │  ← Business Logic
                    │   (GetMoviesHandler)   │
                    └───────────┬────────────┘
                                │
                                │ Call Repository
                                ▼
                    ┌────────────────────────┐
                    │   Repository           │  ← Data Access
                    │   (MovieRepository)    │
                    └───────────┬────────────┘
                                │
                                │ Query Database
                                ▼
                    ┌────────────────────────┐
                    │   EF Core DbContext    │  ← ORM
                    └───────────┬────────────┘
                                │
                                ▼
                    ┌────────────────────────┐
                    │   SQL Server Database  │
                    └────────────────────────┘
                                │
                                │ Return Entities
                                ▼
                    ┌────────────────────────┐
                    │   AutoMapper           │  ← Map to DTOs
                    └───────────┬────────────┘
                                │
                                │ Return DTOs
                                ▼
                    ┌────────────────────────┐
                    │   JSON Response        │
                    └────────────────────────┘
                                │
                                ▼
                    ┌────────────────────────┐
                    │   React Frontend       │
                    └────────────────────────┘
```

---

## ⚡ Event-Driven Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                    USER WATCHES MOVIE                               │
└─────────────────────────────────────────────────────────────────────┘
                                 │
                                 ▼
                    ┌────────────────────────┐
                    │  UpdateWatchHistory    │
                    │  Command Handler       │
                    └───────────┬────────────┘
                                │
                                │ Save to DB
                                ▼
                    ┌────────────────────────┐
                    │  WatchHistory Entity   │
                    └───────────┬────────────┘
                                │
                                │ Raise Event
                                ▼
                    ┌────────────────────────┐
                    │ UserWatchedMovieEvent  │
                    └───────────┬────────────┘
                                │
                    ┌───────────┴───────────┬──────────────┐
                    │                       │              │
                    ▼                       ▼              ▼
        ┌──────────────────┐  ┌──────────────────┐  ┌──────────────┐
        │ Update Movie     │  │ Update User      │  │ Clear Cache  │
        │ Popularity       │  │ Recommendations  │  │ (if needed)  │
        └──────────────────┘  └──────────────────┘  └──────────────┘
```

---

## 🎯 Feature to Entity Mapping

```
┌─────────────────────────────────────────────────────────────────────┐
│                         FEATURES                                    │
└─────────────────────────────────────────────────────────────────────┘

📺 Browse Movies
   └─> Movie Entity
       └─> MovieGenre Entity
           └─> Genre Enum

🎬 Watch Movie
   └─> Movie Entity (VideoUrl)
       └─> WatchHistory Entity (track progress)
           └─> UserWatchedMovieEvent (analytics)

⏯️ Continue Watching
   └─> WatchHistory Entity (LastWatchedPositionSeconds)
       └─> Movie Entity (video details)

❤️ My List
   └─> MyListItem Entity
       └─> Movie Entity
           └─> MovieAddedToListEvent

👥 Watch Party
   └─> Room Entity (synchronized state)
       └─> RoomParticipant Entity (users in room)
           └─> Movie Entity (video being watched)

🔍 Search Movies
   └─> Movie Entity (Title, Description, Cast)
       └─> MovieGenre Entity (filter by genre)

📊 Admin Dashboard
   └─> Movie Entity (CRUD operations)
       └─> MovieUploadedEvent (new content)

🔐 Authentication
   └─> User Entity
       └─> ASP.NET Identity (coming in Phase 3)
```

---

## 🚀 Technology Stack Visualization

```
┌─────────────────────────────────────────────────────────────────────┐
│                         FRONTEND                                    │
├─────────────────────────────────────────────────────────────────────┤
│  React 18  │  Vite  │  Tailwind CSS  │  React Router  │  Axios     │
└─────────────────────────────────────────────────────────────────────┘
                                 │
                                 │ HTTP/REST + WebSockets
                                 ▼
┌─────────────────────────────────────────────────────────────────────┐
│                         BACKEND API                                 │
├─────────────────────────────────────────────────────────────────────┤
│  ASP.NET Core 9  │  JWT Auth  │  Swagger  │  SignalR  │  CORS      │
└─────────────────────────────────────────────────────────────────────┘
                                 │
                    ┌────────────┴────────────┐
                    ▼                         ▼
┌──────────────────────────┐    ┌──────────────────────────┐
│   APPLICATION LAYER      │    │   INFRASTRUCTURE LAYER   │
├──────────────────────────┤    ├──────────────────────────┤
│ MediatR                  │    │ Entity Framework Core    │
│ AutoMapper               │    │ SQL Server               │
│ FluentValidation         │    │ In-Memory Cache          │
│ Commands & Queries       │    │ Hangfire                 │
│ DTOs                     │    │ Azure Blob Storage       │
└──────────────────────────┘    └──────────────────────────┘
                    │                         │
                    └────────────┬────────────┘
                                 ▼
                    ┌────────────────────────┐
                    │    DOMAIN LAYER        │
                    ├────────────────────────┤
                    │ Pure C# Entities       │
                    │ Domain Events          │
                    │ Business Rules         │
                    │ ✅ COMPLETE            │
                    └────────────────────────┘
```

---

## 📦 NuGet Packages (To Be Installed)

```
┌─────────────────────────────────────────────────────────────────────┐
│                    PHASE 2: APPLICATION                             │
├─────────────────────────────────────────────────────────────────────┤
│  ✓ MediatR                                                          │
│  ✓ MediatR.Extensions.Microsoft.DependencyInjection                │
│  ✓ AutoMapper                                                       │
│  ✓ AutoMapper.Extensions.Microsoft.DependencyInjection             │
│  ✓ FluentValidation                                                 │
│  ✓ FluentValidation.DependencyInjectionExtensions                  │
└─────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────┐
│                  PHASE 3: INFRASTRUCTURE                            │
├─────────────────────────────────────────────────────────────────────┤
│  ✓ Microsoft.EntityFrameworkCore                                    │
│  ✓ Microsoft.EntityFrameworkCore.SqlServer                         │
│  ✓ Microsoft.EntityFrameworkCore.Tools                             │
│  ✓ Microsoft.AspNetCore.Identity.EntityFrameworkCore               │
│  ✓ Hangfire                                                         │
│  ✓ Hangfire.SqlServer                                               │
│  ✓ Microsoft.Extensions.Caching.Memory                             │
│  ✓ Azure.Storage.Blobs                                              │
└─────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────┐
│                      PHASE 4: API                                   │
├─────────────────────────────────────────────────────────────────────┤
│  ✓ Microsoft.AspNetCore.Authentication.JwtBearer                   │
│  ✓ Swashbuckle.AspNetCore                                           │
│  ✓ Microsoft.AspNetCore.SignalR                                     │
└─────────────────────────────────────────────────────────────────────┘
```

---

## 🎓 Learning Path Visualization

```
Week 1: Backend Foundation
┌─────┬─────┬─────┬─────┬─────┐
│ Day │ Day │ Day │ Day │ Day │
│  1  │  2  │  3  │  4  │  5  │
├─────┼─────┼─────┼─────┼─────┤
│ ✅  │ 🔜  │     │     │     │
│Phase│Phase│Phase│Phase│Phase│
│  1  │  2  │  3  │  4  │  5  │
│     │     │     │     │     │
│Domain│App │Infra│ API │Real │
│Layer│Layer│Layer│Layer│-time│
└─────┴─────┴─────┴─────┴─────┘

Week 2: Frontend & Deployment
┌─────┬─────┬─────┬─────┬─────┐
│ Day │ Day │ Day │ Day │ Day │
│  1  │  2  │  3  │  4  │  5  │
├─────┼─────┼─────┼─────┼─────┤
│     │     │     │     │     │
│     Phase 6      │     │Phase│
│     React        │     │  7  │
│     Frontend     │     │     │
│                  │     │DevOps│
└─────┴─────┴─────┴─────┴─────┘

Progress: ████░░░░░░░░░░░░░░░░ 14% (1/7 phases)
```

---

## 🎯 Current Status

```
✅ Phase 1: Domain Layer          [████████████████████] 100%
⏳ Phase 2: Application Layer     [░░░░░░░░░░░░░░░░░░░░]   0%
⏳ Phase 3: Infrastructure Layer  [░░░░░░░░░░░░░░░░░░░░]   0%
⏳ Phase 4: API Layer             [░░░░░░░░░░░░░░░░░░░░]   0%
⏳ Phase 5: SignalR               [░░░░░░░░░░░░░░░░░░░░]   0%
⏳ Phase 6: React Frontend        [░░░░░░░░░░░░░░░░░░░░]   0%
⏳ Phase 7: DevOps                [░░░░░░░░░░░░░░░░░░░░]   0%

Overall Progress: ██░░░░░░░░░░░░░░░░░░ 14%
```

---

**Ready for Phase 2?** 🚀

Say "Let's start Phase 2" to continue with MediatR, CQRS, and the Application layer!
