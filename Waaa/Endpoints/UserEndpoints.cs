using Waaa.Application.Interfaces;
using Waaa.Application.Models;

namespace Waaa.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users", (IUserService _userService) =>
            {
                return Results.Ok(_userService.GetUsers());
            })
            .WithTags("User")
            .WithOpenApi();

            app.MapPost("/getblueprint", async (User user, IUserService _userService) =>
            {

                return Results.Ok(await _userService.AddUserAsync(user));
            })
            .WithTags("User")
            .WithOpenApi();

            app.MapPost("/registerwebinar", async (User user, IWebinarRegistrationService webinarRegistrationService) =>
            {
                var result = await webinarRegistrationService.RegisterWebinarAsync(user);
                return result == 0 ? Results.Conflict("The user is already registered.") : Results.Ok(result);
            })
            .WithTags("User")
            .WithOpenApi();
        }
    }
}
