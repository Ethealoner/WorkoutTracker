# Workout Tracker App
[![Build](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackermvc.yml/badge.svg)](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackermvc.yml/badge.svg)
[![Build](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackerwebapi.yml/badge.svg)](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackerwebapi.yml/badge.svg)
[![Build](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackerangular.yml/badge.svg)](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/main_workouttrackerangular.yml/badge.svg)
[![.NET](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/dotnetTests.yml/badge.svg)](https://github.com/Ethealoner/WorkoutTracker/actions/workflows/dotnetTests.yml)

<br/>

This is a solution for creating SPA with ASP.NET Core MVC/Angular, following the principles of Clean Architecture.

## Demo

* [Workout Tracker App MVC](https://workouttrackermvc20230504155820.azurewebsites.net)
* [Workout Tracker App Angular](https://workouttrackerangular.azurewebsites.net)

## Technologies Used

* ASP.NET Core
* Angular
* Entity Framework Core
* MediatR
* FluentAPI
* XUnit
* Azure

## Patterns Used

* Clean Architecture
* Repository pattern with Unit of Work pattern
* CQRS - with MediatR

### Database Configuration

This solution uses Microsoft SQL Server. Add migration from root folder:

`dotnet ef migrations add InitialMigration --verbose --project WorkoutTracker.Infrastructure --startup-project WorkoutTracker.Mvc`

Then run database update:

`dotnet ef database update --project WorkoutTracker.Infrastructure --startup-project WorkoutTracker.Mvc`
