# christmas-secret-gifter-api
## Technical notes
### Format
```
dotnet tool install -g dotnet-format
```
Usage:
```
dotnet-format .\Christmas.Secret.Gifter.API\
```

### TypeGen
Install: 
``` 
dotnet tool install --global dotnet-typegen
```
Rebuild the solution and then being in the root directory execute:
```
dotnet-typegen --project-folder  ./Christmas.Secret.Gifter.Domain generate
```
### Database.SQLite:
navigate to the database project directory first.
Then execute as follows:
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet ef database update --connection "Data Source=gifter.db"
```

### Known issues
Nuget: invalid data while decoding:
```
dotnet nuget locals all --clear
```

### Algorithm
```
https://developers.google.com/optimization/assignment/assignment_example
```
