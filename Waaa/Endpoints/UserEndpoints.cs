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
        }
    }
}
