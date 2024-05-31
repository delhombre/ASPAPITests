# Exploring API building with ASP.NET Core

## Overview

The DemoASPTest project is a multi-layered ASP.NET solution designed for demonstration purposes. It includes several projects organized into different layers such as Domain, Data Access Layer (DAL), Business Logic Layer (BLL), and an API layer. Additionally, there is a database project for managing database operations.

## Projects

1. DemoASPTest.Domain
   - Purpose: Contains the domain models and business rules.
   - Framework: .NET 8.0
   - Key Features: Implicit usings and nullable reference types are enabled.
2. DemoASPTest.DAL
   - Purpose: Manages data access operations.
3. DemoASPTest.BLL
   - Purpose: Contains business logic.
4. DemoASPTest.API
   - Purpose: Exposes business logic and data access as web services.
5. DemoASPTest.DB
   - Purpose: Manages the SQL database schema and data.
   - Key Features: Uses Microsoft Build SQL SDK for project management and includes scripts for database population.

## Solution Configuration

- Visual Studio Version: 17.0.31903.59
- Minimum Visual Studio Version Required: 10.0.40219.1
- Build Configurations: Debug and Release for Any CPU.

## Repository Structure

- [DemoASPTest.Domain](/DemoASPTest.Domain/): Domain logic
- [DemoASPTest.DAL](/DemoASPTest.DAL/): Data access layer
- [DemoASPTest.BLL](/DemoASPTest.BLL/): Business logic layer
- [DemoASPTest.API](/DemoASPTest.API/): API endpoints
- [DemoASPTest.DB](/DemoASPTest.DB/): Database project with SQL scripts

## Building the Project

To build the project, open DemoASPTest.sln in Visual Studio 2017 or later and select the desired build configuration.

## Additional Notes

- Ensure that all necessary NuGet packages and SDKs are installed before building the projects.
- Database scripts are located under the Scripts folder in the DemoASPTest.DB project.
