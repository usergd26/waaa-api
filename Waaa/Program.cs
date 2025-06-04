using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Waaa.Application.Interfaces;
using Waaa.Application.Services;
using Waaa.Domain;
using Waaa.Domain.Interfaces;
using Waaa.Domain.Repositories;
using Waaa.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseWhen(context => context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        var headers = context.Request.Headers["Authorization"];
        if (string.IsNullOrWhiteSpace(headers))
        {
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        var encodedUsernamePassword = headers.ToString().Replace("Basic ", "");
        var decodedUsernamePassword = System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(encodedUsernamePassword));

        var parts = decodedUsernamePassword.Split(':', 2);
        var username = parts[0];
        var password = parts[1];

        var validUsername = "admin";
        var validPassword = "password";

        if (username != validUsername || password != validPassword)
        {
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await next.Invoke();
    });
});


// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(); // Optional: You can customize the UI here


app.MapGet("/users", (IUserService _userService) =>
{
    return Results.Ok(_userService.GetUsers());
})
.WithName("User")
.WithOpenApi();

app.MapPost("/getblueprint", (User user, IUserService _userService) =>
{

    return Results.Ok(_userService.AddUser(user));
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

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // To serve Swagger UI at the app root
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
