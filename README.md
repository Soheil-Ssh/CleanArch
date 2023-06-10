# Clean Architecture Sample

This project is a simple example of Clean Architecture in .NET.

## Installation and Setup

To run this project, you need to first install a version of .NET Core. Then, clone this repository. To run the project on your system, use the following command:
dotnet run

## Project Structure

The project is structured as follows:
CleanArch/
├── .gitignore
├── CleanArch.sln
├── CleanArch/
│   ├── CleanArch.csproj
│   ├── Program.cs
├── CleanArch.Domain/
│   ├── Entities/
│   │   ├── Product.cs
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   ├── Services/
│   │   ├── ProductService.cs
│   ├── CleanArch.Domain.csproj
├── CleanArch.Infrastructure/
│   ├── Data/
│   │   ├── Context/
│   │   │   ├── AppDbContext.cs
│   │   ├── Repositories/
│   │   │   ├── Repository.cs
│   ├── CleanArch.Infrastructure.csproj
├── CleanArch.Application/
│   ├── Interfaces/
│   │   ├── IProductService.cs
│   ├── CleanArch.Application.csproj
├── CleanArch.WebApi/
│   ├── Controllers/
│   │   ├── ProductController.cs
│   ├── CleanArch.WebApi.csproj
└── README.md

The project consists of five projects: `CleanArch.Application`, `CleanArch.Common`, `CleanArch.Core`, `CleanArch.Infrastructure`, and `CleanArch.Web`. The file structure is shown above.

### CleanArch.Application

This project contains the application services used for processing data. The `Interfaces` folder contains the `IProductService` interface, and the `Services` folder contains the `ProductService` class.

### CleanArch.Common

This project contains common utilities that can be used across the solution.

### CleanArch.Core

This project contains the domain entities.

### CleanArch.Infrastructure

This project contains the classes related to data access. The `Data` folder contains the `Context` folder, which contains the `AppDbContext` class. The `Data` folder also contains the `Repositories` folder, which contains the `Repository` class.

### CleanArch.IoC

This project contains the dependency injection configuration for the solution.

### CleanArch.Web

This project contains the web controllers and settings.

## Documentation

For documentation and instructions on how to use this project, see the `docs/README.md` file.

## License

This project is licensed under the [MIT License](LICENSE).
