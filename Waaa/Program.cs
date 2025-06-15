using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Waaa.API.Endpoints;
using Waaa.Domain;
using Waaa.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)            
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) 
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

builder.Services.AddInfrastructure();
var app = builder.Build();


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


app.MapUserEndpoints();

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
