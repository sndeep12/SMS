dotnet tool install --global dotnet-ef
dotnet ef migrations add User --project ../SchoolManagement.Infrastructure --startup-project . --context SchoolManagement.Infrastructure.Persistence.AppDbContext
dotnet ef database update --project ../SchoolManagement.Infrastructure --startup-project .