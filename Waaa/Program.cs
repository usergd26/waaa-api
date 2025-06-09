using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Waaa.Application.Interfaces;
using Waaa.Application.Services;
using Waaa.Domain;
using Waaa.Domain.Interfaces;
using Waaa.Domain.Repositories;
using Waaa.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()                   // Default minimum level
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)            // Override Microsoft logs to Warning+
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // Override ASP.NET Core logs to Warning+
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MySQL(connectionString, "Logs")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,new MySqlServerVersion(new Version(8, 0, 31)),
    mySqlOptions =>
    {
        mySqlOptions.EnableRetryOnFailure();
    }));

builder.Services.AddEndpointsApiExplorer();

// Enable Swagger middleware
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add BasicAuth definition
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter your username and password for Basic Auth"
    });

    // Add BasicAuth requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB migration failed: " + ex.Message);
        throw;
    }
}


app.MapGet("/users", (IUserService _userService) =>
{
    return Results.Ok(_userService.GetUsers());
})
.WithName("User")
.WithOpenApi();

app.MapPost("/getblueprint", async (User user, IUserService _userService) =>
{

    return Results.Ok( await _userService.AddUserAsync(user));
})
.WithName("Blueprint")
.WithOpenApi();

app.MapGet("/payment/{amount}", (decimal amount) =>
{
    return Results.Ok(new
    {
        Message = $"Payment successful! Thank you for your payment of {amount}.",
        Time = DateTime.UtcNow
    });
})
.WithName("Payment")
.WithOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
