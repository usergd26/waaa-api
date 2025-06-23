using Waaa.Application.Interfaces;
using Waaa.Application.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Waaa.API.Endpoints
{
    public static class WebinarEndpoints
    {
        private const string tagGroup = "Webinar";
        public static void MapWebinarEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/registerwebinar", async (WebinarDto webinarDto, IWebinarService webinarRegistrationService) =>
            {
                var result = await webinarRegistrationService.RegisterWebinarAsync(webinarDto);
                return result == 0 ? Results.Conflict("The user is already registered.") : Results.Ok(result);
            })
            .WithTags(tagGroup)
            .WithOpenApi();

            app.MapPatch("/addpayment", async (int id, IWebinarService webinarRegistrationService) =>
            {
                var result = await webinarRegistrationService.AddPaymentAsync(id);
                return !result ? Results.BadRequest("Invalid User") : Results.Ok(result);
            })
            .WithTags(tagGroup)
            .WithOpenApi();

            app.MapGet("/webinarregistrations", async (IWebinarService webinarRegistrationService) =>
            {
                var result = await webinarRegistrationService.GetWebinarRegistrationsAsync();
                return Results.Ok(result);
            })
            .WithTags(tagGroup)
            .WithOpenApi();
        }
    }
}
