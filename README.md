# FamilyFlow

FamilyFlow is an ASP.NET Core MVC web application for managing family members, household tasks, and scheduled events in one place.

## Overview

The application provides:
- Family member management (create, list, details, edit, delete)
- House task assignment per family member
- Event scheduling per family member, including optional accompanying adult
- A unified schedule view that combines tasks and events
- ASP.NET Core Identity authentication (register, login, logout)

Most business pages are protected with `[Authorize]`, so users must sign in.

## Tech Stack

- .NET 8 (`net8.0`)
- ASP.NET Core MVC + Razor Views
- ASP.NET Core Identity (default UI)
- Entity Framework Core 8
- SQL Server provider (configured by default)
- Bootstrap + jQuery (via `wwwroot/lib`)

## Solution Structure

- `FamilyFlow.sln` - solution file
- `FamilyFlow/` - web application project
- `FamilyFlow/Program.cs` - app startup, DI setup, middleware, routes
- `FamilyFlow/Data/` - EF Core DbContext, entities, model configurations, migrations
- `FamilyFlow/Controllers/` - MVC controllers for feature flows
- `FamilyFlow/ViewModels/` - input/output models used by views
- `FamilyFlow/Views/` - Razor views
- `FamilyFlow/Areas/Identity/` - scaffolded Identity pages
- `FamilyFlow/Common/` - constants and helper extensions

## Domain Model

### FamilyMember
- `Id`, `Name`, `Role`, `Age`
- Relations:
  - one-to-many to `HouseTask`
  - one-to-many to `ScheduleEvent` (as main participant)
  - one-to-many to `ScheduleEvent` (as accompanying adult)

### HouseTask
- `Id`, `Title`, `Description`, `DueDate`, `IsCompleted`, `FamilyMemberId`
- Belongs to one `FamilyMember`

### ScheduleEvent
- `Id`, `Title`, `StartTime`, `EndTime`, `FamilyMemberId`, `AccompanyingAdultId?`
- Belongs to one `FamilyMember`
- Optional link to an accompanying adult (`FamilyMember`)

## Business Rules (Current Implementation)

- Family member name and task/event titles have length validation rules.
- Family member age is validated in range `0..120`.
- Task due date must be in the future.
- Event start time must be earlier than end time.
- If a family member is age `<= 12`, an accompanying adult is required for event create/edit.
- Deleting a family member cascades to their tasks/events.
- Deleting an accompanying adult is restricted while referenced by events.

## Seed Data

Seed data is configured through EF Core model configurations:
- 4 family members (`Alice`, `Bob`, `Charlie`, `Daisy`)
- 6 house tasks
- 4 schedule events

## Prerequisites

Make sure you have the following installed before running the project:

- .NET SDK 8.0+
- SQL Server LocalDB (or SQL Server instance)

## Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/Marina-Antonova/Family_Flow
cd Family-Flow
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Apply database migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

The app will be available at `https://localhost:5205` or `http://localhost:7103`.

## Configuration

Key settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string-here"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

## Database Setup

Migrations are in `FamilyFlow/Data/Migrations`.

```powershell
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Main User Flows

1. Register and log in.
2. Open `Family Members` and create members.
3. Open a member `Details` page to add/edit/delete tasks and events.
4. Open `Schedule` to see tasks and events combined chronologically.

## Contact

**Marina Antonova** - https://github.com/Marina-Antonova

Project Link: https://github.com/Marina-Antonova/Family_Flow

*Built as part of the **ASP.NET Fundamentals** *course.*