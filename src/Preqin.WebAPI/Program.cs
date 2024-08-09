using Microsoft.EntityFrameworkCore;
using Preqin.Application.Repositories;
using Preqin.Infrastructure.Data;
using Preqin.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins(allowedOrigins)
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
});

// Register the DbContext with SQLite provider
builder.Services.AddDbContext<PreqinDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IInvestorsRepository, InvestorsRepository>();
builder.Services.AddScoped<ICommitmentRepository, CommitmentRepository>();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter("Preqin.WebAPI.Middleware", LogLevel.Information);
builder.Logging.AddFilter((category, logLevel) =>
{
    return category.StartsWith("Preqin.WebAPI.Middleware");
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
