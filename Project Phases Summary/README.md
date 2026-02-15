# 🎬 Netflix Clone - Clean Architecture

A full-stack Netflix clone built with **ASP.NET Core**, **React**, and **Azure Free Tier**, following **Clean Architecture** principles and modern development practices.

## 🏗️ Architecture

This project follows **Clean Architecture** (also known as Onion Architecture) with clear separation of concerns:

```
┌─────────────────────────────────────────┐
│         NetflixClone.API                │  ← Entry Point (Controllers, Hubs)
│  ┌───────────────────────────────────┐  │
│  │   NetflixClone.Infrastructure     │  │  ← External Concerns (DB, Cache, SignalR)
│  │  ┌─────────────────────────────┐  │  │
│  │  │  NetflixClone.Application   │  │  │  ← Business Logic (MediatR, DTOs)
│  │  │  ┌───────────────────────┐  │  │  │
│  │  │  │  NetflixClone.Domain  │  │  │  │  ← Core Entities & Rules
│  │  │  └───────────────────────┘  │  │  │
│  │  └─────────────────────────────┘  │  │
│  └───────────────────────────────────┘  │
└─────────────────────────────────────────┘
```

### Dependency Flow (Clean Architecture Rule)
- **Domain** → No dependencies (pure business entities)
- **Application** → Depends on Domain only
- **Infrastructure** → Depends on Application (implements interfaces)
- **API** → Depends on Application & Infrastructure (composition root)

## 📁 Project Structure

```
netflix-clone/
├── src/
│   ├── NetflixClone.Domain/          # Core business entities
│   │   ├── Entities/                 # Movie, User, WatchHistory, etc.
│   │   ├── Events/                   # Domain events (UserWatchedMovieEvent)
│   │   ├── Enums/                    # Genre, UserRole, etc.
│   │   └── Common/                   # Base entities, value objects
│   │
│   ├── NetflixClone.Application/     # Business logic & use cases
│   │   ├── Commands/                 # Write operations (CreateMovie, AddToList)
│   │   ├── Queries/                  # Read operations (GetMovies, SearchMovies)
│   │   ├── Handlers/                 # MediatR command/query handlers
│   │   ├── DTOs/                     # Data transfer objects
│   │   ├── Interfaces/               # Repository & service contracts
│   │   └── Mappings/                 # AutoMapper profiles
│   │
│   ├── NetflixClone.Infrastructure/  # External services & data access
│   │   ├── Persistence/              # EF Core DbContext, Repositories
│   │   ├── Caching/                  # In-Memory cache implementation
│   │   ├── SignalR/                  # Real-time hubs (WatchPartyHub)
│   │   ├── Hangfire/                 # Background jobs
│   │   ├── Storage/                  # Azure Blob / Local file storage
│   │   └── Identity/                 # ASP.NET Identity configuration
│   │
│   └── NetflixClone.API/             # Web API & entry point
│       ├── Controllers/              # REST API endpoints
│       ├── Hubs/                     # SignalR hubs
│       ├── Middleware/               # Error handling, logging
│       └── Program.cs                # DI container setup
│
├── client/                           # React frontend (to be created)
└── NetflixClone.sln                  # Solution file
```

## 🛠️ Tech Stack

### Backend
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core** - ORM for database access
- **MediatR** - CQRS pattern implementation
- **ASP.NET Identity** - Authentication & authorization
- **SignalR** - Real-time communication (Watch Party)
- **Hangfire** - Background job processing
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation

### Frontend (Coming Soon)
- **React 18** with **Vite**
- **Tailwind CSS** - Styling
- **Axios** - HTTP client
- **SignalR Client** - Real-time features

### Database
- **SQL Server Express** (local development)
- **SQLite** (fallback/testing)

### Cloud (Free Tier)
- **Azure Static Web Apps** - Frontend hosting
- **Azure App Service F1** - Backend hosting
- **Azure Blob Storage** - Video storage (5GB free)

## 🎯 Features

### Phase 1 - Core Features ✅ (In Progress)
- [ ] User authentication (Register/Login with JWT)
- [ ] Browse movies
- [ ] Movie details page
- [ ] Video player
- [ ] Search functionality
- [ ] Admin movie upload

### Phase 2 - User Features
- [ ] Watch history tracking
- [ ] My List (favorites)
- [ ] Continue watching row

### Phase 3 - Advanced Features
- [ ] **SignalR Watch Party** - Synchronized viewing with friends
- [ ] **Hangfire Background Jobs** - Daily recommendations, popularity updates
- [ ] **In-Memory Caching** - Fast API responses
- [ ] **MediatR Domain Events** - Event-driven architecture

### Phase 4 - DevOps
- [ ] GitHub Actions CI/CD
- [ ] Docker containerization
- [ ] Azure deployment

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js 18+ (for frontend)
- SQL Server Express or SQLite
- Visual Studio 2022 / VS Code / Rider

### Running the Backend

1. **Clone the repository**
   ```bash
   cd c:\Users\ganes\Desktop\netflix-clone
   ```

2. **Build the solution**
   ```bash
   dotnet build
   ```

3. **Run the API**
   ```bash
   dotnet run --project src\NetflixClone.API
   ```

4. **Access Swagger UI**
   ```
   https://localhost:5001/swagger
   ```

## 📚 Learning Objectives

By building this project, you'll learn:

✅ **Clean Architecture** - Proper layering and dependency management  
✅ **CQRS Pattern** - Command Query Responsibility Segregation with MediatR  
✅ **Domain-Driven Design** - Entities, value objects, domain events  
✅ **Repository Pattern** - Data access abstraction  
✅ **JWT Authentication** - Secure API endpoints  
✅ **SignalR** - Real-time bi-directional communication  
✅ **Hangfire** - Background job scheduling  
✅ **Caching Strategies** - Performance optimization  
✅ **API Gateway Pattern** - Aggregated endpoints  
✅ **Azure Deployment** - Cloud hosting on free tier  
✅ **CI/CD Pipelines** - Automated testing and deployment  

## 📖 Next Steps

1. **Set up Domain Entities** - Create Movie, User, Genre models
2. **Configure EF Core** - Database context and migrations
3. **Implement Authentication** - ASP.NET Identity + JWT
4. **Build CQRS Handlers** - MediatR commands and queries
5. **Create API Controllers** - RESTful endpoints
6. **Add SignalR Hubs** - Real-time watch party
7. **Configure Hangfire** - Background jobs
8. **Build React Frontend** - UI components and pages
9. **Deploy to Azure** - Free tier hosting

## 📝 Current Status

✅ Solution structure created  
✅ Clean Architecture layers established  
✅ Project dependencies configured  
✅ Solution builds successfully  

**Next:** Create Domain entities and database models

---

**Built with ❤️ for learning modern .NET development**
