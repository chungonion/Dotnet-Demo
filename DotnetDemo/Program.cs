using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddNpgsql<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"),(builder) =>
{
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    builder.CommandTimeout(5);
    // builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
});
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();