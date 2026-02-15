# 🎯 Netflix Clone - Quick Reference Guide

## 📁 Project Structure

```
netflix-clone/
├── src/
│   ├── NetflixClone.Domain/
│   │   ├── Common/
│   │   │   └── BaseEntity.cs
│   │   ├── Entities/
│   │   │   ├── Movie.cs
│   │   │   ├── User.cs
│   │   │   ├── MovieGenre.cs
│   │   │   ├── WatchHistory.cs
│   │   │   ├── MyListItem.cs
│   │   │   ├── Room.cs
│   │   │   └── RoomParticipant.cs
│   │   ├── Enums/
│   │   │   ├── Genre.cs
│   │   │   ├── UserRole.cs
│   │   │   └── ContentRating.cs
│   │   └── Events/
│   │       ├── BaseDomainEvent.cs
│   │       ├── UserWatchedMovieEvent.cs
│   │       ├── MovieUploadedEvent.cs
│   │       └── MovieAddedToListEvent.cs
│   │
│   ├── NetflixClone.Application/      (Next: MediatR, DTOs)
│   ├── NetflixClone.Infrastructure/   (Next: EF Core, Repositories)
│   └── NetflixClone.API/              (Next: Controllers, JWT)
│
├── NetflixClone.sln
├── README.md
└── PHASE1_COMPLETE.md
```

## 🏗️ Clean Architecture Layers

| Layer | Purpose | Dependencies | Examples |
|-------|---------|--------------|----------|
| **Domain** | Core business entities | NONE | Movie, User, Events |
| **Application** | Business logic, use cases | Domain only | Commands, Queries, DTOs |
| **Infrastructure** | External services, data | Application | EF Core, Blob, Cache |
| **API** | Entry point, controllers | App + Infra | Controllers, Hubs |

## 📊 Entity Relationships

```
┌─────────┐         ┌──────────────┐         ┌───────┐
│  User   │────<───│ WatchHistory │───>────│ Movie │
└─────────┘         └──────────────┘         └───────┘
     │                                            │
     │              ┌──────────┐                  │
     └─────<───────│ MyListItem │────>────────────┘
     │              └──────────┘                  │
     │                                            │
     │         ┌──────────────────┐               │
     └────<───│ RoomParticipant  │               │
              └──────────────────┘               │
                      │                          │
                      ↓                          │
                  ┌──────┐                       │
                  │ Room │──────────────────────>┘
                  └──────┘
                      
                  ┌─────────────┐
                  │ MovieGenre  │
                  └─────────────┘
                       │
                       ↓
                  ┌─────────┐
                  │  Genre  │ (enum)
                  └─────────┘
```

## 🎬 Core Entities Cheat Sheet

### Movie
```csharp
- Id, Title, Description
- ReleaseYear, DurationMinutes
- VideoUrl, ThumbnailUrl, BannerUrl
- Rating (ContentRating enum)
- AverageRating, PopularityScore, ViewCount
- Director, Cast
```

### User
```csharp
- Id, Email, FirstName, LastName
- ProfilePictureUrl
- SubscriptionTier
- LastLoginAt
```

### WatchHistory
```csharp
- UserId, MovieId
- LastWatchedPositionSeconds
- PercentageWatched
- IsCompleted
- LastWatchedAt
```

### MyListItem
```csharp
- UserId, MovieId
- AddedAt
```

### Room (Watch Party)
```csharp
- RoomCode, Name
- HostUserId, MovieId
- CurrentPositionSeconds
- IsPlaying, IsActive
```

### RoomParticipant
```csharp
- RoomId, UserId
- JoinedAt, LeftAt
- IsConnected
```

## ⚡ Domain Events

| Event | Trigger | Purpose |
|-------|---------|---------|
| `UserWatchedMovieEvent` | User watches movie | Update history, popularity, recommendations |
| `MovieUploadedEvent` | Admin uploads movie | Clear cache, notify users |
| `MovieAddedToListEvent` | User adds to My List | Update preferences, recommendations |

## 🛠️ Common Commands

### Build Solution
```bash
dotnet build
```

### Run API
```bash
dotnet run --project src\NetflixClone.API
```

### Add NuGet Package
```bash
dotnet add src\NetflixClone.Application package MediatR
```

### Create Migration (later)
```bash
dotnet ef migrations add InitialCreate -p src\NetflixClone.Infrastructure -s src\NetflixClone.API
```

## 🎯 Feature Mapping

| Feature | Entities Used | Events Triggered |
|---------|---------------|------------------|
| Browse Movies | Movie, MovieGenre | - |
| Watch Movie | Movie, WatchHistory | UserWatchedMovieEvent |
| My List | MyListItem, Movie | MovieAddedToListEvent |
| Continue Watching | WatchHistory, Movie | - |
| Watch Party | Room, RoomParticipant, Movie | - |
| Upload Movie | Movie | MovieUploadedEvent |

## 📚 Next Phase Checklist

### Phase 2: Application Layer
- [ ] Install MediatR NuGet package
- [ ] Create Commands (CreateMovie, AddToMyList, etc.)
- [ ] Create Queries (GetMovies, SearchMovies, etc.)
- [ ] Create DTOs (MovieDto, UserDto, etc.)
- [ ] Create MediatR Handlers
- [ ] Define Repository Interfaces
- [ ] Add FluentValidation

### Phase 3: Infrastructure Layer
- [ ] Install EF Core packages
- [ ] Create DbContext
- [ ] Configure entity relationships
- [ ] Implement repositories
- [ ] Add database migrations
- [ ] Configure ASP.NET Identity
- [ ] Implement caching

### Phase 4: API Layer
- [ ] Create Controllers
- [ ] Configure JWT authentication
- [ ] Add Swagger
- [ ] Create SignalR hubs
- [ ] Add middleware (error handling)

## 💡 Design Patterns Used

✅ **Clean Architecture** - Dependency inversion  
✅ **Domain-Driven Design** - Rich domain model  
✅ **Repository Pattern** - Data access abstraction (coming in Phase 3)  
✅ **CQRS** - Command Query Responsibility Segregation (coming in Phase 2)  
✅ **Event-Driven Architecture** - Domain events  
✅ **Soft Delete Pattern** - IsDeleted flag  

## 🔗 Useful Resources

- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)

---

**Status**: ✅ Phase 1 Complete | 🚧 Phase 2 Ready to Start
