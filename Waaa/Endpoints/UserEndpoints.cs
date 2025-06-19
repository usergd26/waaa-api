using Waaa.Application.Interfaces;

namespace Waaa.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async (IUserService userService) =>
            {
                return Results.Ok( await userService.GetUsersAsync());
            })
            .WithTags("User")
            .WithOpenApi()
            .RequireAuthorization("AdminOnly");
        }
    }
}
