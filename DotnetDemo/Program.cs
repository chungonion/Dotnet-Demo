using Microsoft.EntityFrameworkCore;
using WebApplication1.GraphQL.DataLoaders;
using WebApplication1.GraphQL.Mutations;
using WebApplication1.GraphQL.Queries;
using WebApplication1.GraphQL.Types;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddNpgsql<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"),(builder) =>
// {
//     builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
//     builder.CommandTimeout(5);
//     // builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
// });

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
    options => options.UseNpgsql(
        configuration.GetConnectionString("DefaultConnection"),
        builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); }));
builder.Services.AddScoped(p =>
    p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    // .InitializeOnStartup()
    .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<StaffQueries>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<StaffMutation>()
    .AddType<StaffType>()
    .AddGlobalObjectIdentification()
    // .AddAuthorization()
    // .AddProjections()
    // .AddFiltering()
    // .AddSorting()
    // .AddInMemorySubscriptions()
    .AddDataLoader<StaffRoleDataLoader>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();