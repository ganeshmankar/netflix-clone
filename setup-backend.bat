@echo off
echo Creating Netflix Clone Solution Structure...
echo.

REM Create solution
dotnet new sln -n NetflixClone

REM Create src directory
mkdir src

REM Create Domain Layer (Class Library)
dotnet new classlib -n NetflixClone.Domain -o src\NetflixClone.Domain
dotnet sln add src\NetflixClone.Domain\NetflixClone.Domain.csproj

REM Create Application Layer (Class Library)
dotnet new classlib -n NetflixClone.Application -o src\NetflixClone.Application
dotnet sln add src\NetflixClone.Application\NetflixClone.Application.csproj

REM Create Infrastructure Layer (Class Library)
dotnet new classlib -n NetflixClone.Infrastructure -o src\NetflixClone.Infrastructure
dotnet sln add src\NetflixClone.Infrastructure\NetflixClone.Infrastructure.csproj

REM Create API Layer (Web API)
dotnet new webapi -n NetflixClone.API -o src\NetflixClone.API
dotnet sln add src\NetflixClone.API\NetflixClone.API.csproj

REM Add project references (Clean Architecture dependencies)
dotnet add src\NetflixClone.Application\NetflixClone.Application.csproj reference src\NetflixClone.Domain\NetflixClone.Domain.csproj
dotnet add src\NetflixClone.Infrastructure\NetflixClone.Infrastructure.csproj reference src\NetflixClone.Application\NetflixClone.Application.csproj
dotnet add src\NetflixClone.API\NetflixClone.API.csproj reference src\NetflixClone.Application\NetflixClone.Application.csproj
dotnet add src\NetflixClone.API\NetflixClone.API.csproj reference src\NetflixClone.Infrastructure\NetflixClone.Infrastructure.csproj

echo.
echo ✅ Solution structure created successfully!
echo.
echo Project structure:
echo - NetflixClone.Domain (Core entities and business logic)
echo - NetflixClone.Application (Use cases, DTOs, MediatR handlers)
echo - NetflixClone.Infrastructure (EF Core, SignalR, Hangfire, Caching)
echo - NetflixClone.API (Controllers, Hubs, Entry point)
echo.
pause
