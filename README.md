# FamilyFlow

FamilyFlow is an ASP.NET Core MVC web application for organizing family members, house tasks, and schedule events in one place.

## What the project includes

- family creation and family overview
- family member management
- house tasks for each family member
- schedule events with optional accompanying adult
- multiple participants in one event
- combined family schedule
- ASP.NET Core Identity authentication
- separate admin area for management actions

## Tech Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap
- NUnit

## Project Structure

- `FamilyFlow/` - main web project
- `FamilyFlow.Data/` - DbContext, configurations, migrations
- `FamilyFlow.Data.Models/` - entity models
- `FamilyFlow.Data.Seeding/` - roles and users seeding
- `FamilyFlow.Serices.Core/` - business logic
- `FamilyFlow.Web.ViewModels/` - view models
- `ScheduleService.Tests/` - tests

## Main Rules

- task due date cannot be in the past
- event start time must be before end time
- if a family member is 12 or younger, an accompanying adult is required
- the event creator is not shown in the participants dropdown
- schedule shows only the current family
- `Mother` and `Father` seeded users are admins

## Seeded Data

The project contains seeded:
- family
- family members
- users
- roles
- house tasks
- schedule events
- event participants

## Demo Accounts

### Admin
- `mother@familyflow.com` / `Mother123!`
- `father@familyflow.com` / `Father123!`

### User
- `son@familyflow.com` / `Son123!`
- `daughter@familyflow.com` / `Daughter123!`

## How to run

### 1. Clone the repository

```bash
git clone https://github.com/Marina-Antonova/Family_Flow
cd Family_Flow
```

### 2. Restore packages

```bash
dotnet restore
```

### 3. Update the database

```bash
dotnet ef database update
```

### 4. Run the project

```bash
dotnet run --project FamilyFlow/FamilyFlow.Web.csproj
```

## Contact

**Marina Antonova** - https://github.com/Marina-Antonova

Project Link: https://github.com/Marina-Antonova/Family_Flow

*Built as part of the **ASP.NET Fundamentals** course *
