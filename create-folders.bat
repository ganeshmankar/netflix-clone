@echo off
echo Creating folder structure...

REM Domain Layer
mkdir src\NetflixClone.Domain\Entities
mkdir src\NetflixClone.Domain\Events
mkdir src\NetflixClone.Domain\Enums
mkdir src\NetflixClone.Domain\Common

REM Application Layer
mkdir src\NetflixClone.Application\Commands
mkdir src\NetflixClone.Application\Queries
mkdir src\NetflixClone.Application\Handlers
mkdir src\NetflixClone.Application\DTOs
mkdir src\NetflixClone.Application\Interfaces
mkdir src\NetflixClone.Application\Mappings

REM Infrastructure Layer
mkdir src\NetflixClone.Infrastructure\Persistence
mkdir src\NetflixClone.Infrastructure\Caching
mkdir src\NetflixClone.Infrastructure\SignalR
mkdir src\NetflixClone.Infrastructure\Hangfire
mkdir src\NetflixClone.Infrastructure\Storage
mkdir src\NetflixClone.Infrastructure\Identity

REM API Layer
mkdir src\NetflixClone.API\Hubs
mkdir src\NetflixClone.API\Middleware

echo ✅ Folder structure created successfully!
