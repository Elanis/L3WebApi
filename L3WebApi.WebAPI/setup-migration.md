# Installation de dotnet ef
> dotnet tool install --global dotnet-ef
> dotnet-ef -v

# Parametrage des secrets
Clic droit sur csproj > Manage User Secrets
```
{
  "SQLConnectionString": "Host=localhost;Port=5432;Database=l3webapi;Username=l3webapi;Password=PASSWORD"
}
```

# Creation d'une migration
cd L3WebApi.DataAccess
dotnet-ef migrations add Initial -s ../L3WebApi.WebAPI
