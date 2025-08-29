# CleanArchSample (MySQL, .NET 8, WebAPI)

## How to run
1) Install EF tools: `dotnet tool install --global dotnet-ef`
2) From the `src/CleanArchSample.Api` folder run:
   - `dotnet restore`
   - Update `appsettings.json` with your MySQL credentials
   - Create migration:
     ```
     dotnet ef migrations add Initial --project ../CleanArchSample.Infrastructure --startup-project . --context CleanArchSample.Infrastructure.Persistence.AppDbContext
     dotnet ef database update --project ../CleanArchSample.Infrastructure --startup-project .
     ```
   - Run API:
     ```
     dotnet run
     ```

## Projects
- Api → Application, Infrastructure
- Application → Domain
- Infrastructure → Application, Domain
- Domain → (none)
