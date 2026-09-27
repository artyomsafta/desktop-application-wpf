# Desktop Application WPF

A Windows desktop application for managing courses, groups, teachers, and students. It supports maintaining student records and importing or exporting student lists.

## Features

- Manage groups, teachers, and students.
- Browse students by course and group.
- Import and export student lists as CSV files.
- Export student lists as DOCX and PDF documents.

## Technologies

- .NET 9 and WPF
- CommunityToolkit.Mvvm
- Entity Framework Core 9 with SQL Server LocalDB
- CsvHelper, Open XML SDK, and QuestPDF
- MSTest and FluentAssertions

## Solution structure

- `Task8-WPF.UI` - WPF user interface and view models.
- `Task8-WPF.BAL` - business services, DTOs, and file export/import services.
- `Task8-WPF.DAL` - EF Core entities, migrations, configuration, and seed data.
- `Task8-WPF.UnitTests` - unit tests.

## Prerequisites

- Windows
- .NET 9 SDK
- SQL Server LocalDB

## Build and run

From the repository root:

```powershell
dotnet restore Task8-DesktopApplicationWPF/Task8-DesktopApplicationWPF.sln
dotnet build Task8-DesktopApplicationWPF/Task8-DesktopApplicationWPF.sln
dotnet run --project Task8-DesktopApplicationWPF/Task8-WPF.UI/Task8-WPF.UI.csproj
```

Run tests with:

```powershell
dotnet test Task8-DesktopApplicationWPF/Task8-DesktopApplicationWPF.sln
```

## Database

The application connects to the named SQL Server LocalDB database `wpfappdb`. On startup, Entity Framework Core applies pending migrations and seeds initial course, group, teacher, and student data when the database is empty.
