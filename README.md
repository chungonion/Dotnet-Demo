# Dotnet Demo
This demo includes demonstration on using .NET 6/7 with ...
- RESTful API
- Usage of various attributes (e.g. `Route`, `HttpGet`, `FromQuery`, `FromBody`, `FromRoute` and so on...)
- CRUD with EF Core
- Dependency Injection
- Middleware

## Setup

### EF Core
#### Database
Since MS SQL Server requires `$$$$`, I have opted for [PostgreSQL](https://www.postgresql.org/) in this demo instead. Use [Postgres App](https://postgresapp.com/) for hassle-free installation, setup and execution of PostgreSQL. To access the database's content, either use the `database` tool bundled in [Rider](https://www.jetbrains.com/rider/), [DataGrip](https://www.jetbrains.com/datagrip/) or [PgAdmin](https://www.pgadmin.org/). [Default username and password for database is admin](https://stackoverflow.com/a/69649419/7761918) and the port is `5432`. Change the connection string with your username and password in `appsettings.json` / `appsettings.Development.json`   

#### Setup
Apply update to database  
`dotnet ef database update`

Try yourself on creating migration  
Assuming the migration is named `init`, and put the migrations into folder of `Migrations`  
`dotnet ef migrations add init -o Migrations`

Remove migration named `init`
`dotnet ef migrations remove init`

⚠️ *Do Not forget to have some initial data in `StaffRoles` table first before testing the apis!*
```
INSERT INTO public."StaffRoles" ("RoleId", "RoleName") VALUES (1, 'Senior Software Engineer');
INSERT INTO public."StaffRoles" ("RoleId", "RoleName") VALUES (2, 'Software Engineer');
```
