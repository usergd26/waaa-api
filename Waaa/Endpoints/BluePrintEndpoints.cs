using Waaa.Application.Interfaces;
using Waaa.Application.Dto;

namespace Waaa.API.Endpoints
{
    public static class BluePrintEndpoints
    {
        private const string endpointGroup = "BluePrint";
        public static void MapBluePrintEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/blueprint", async (BluePrintDto bluePrint, IBluePrintService bluePrintService) =>
            {

                return Results.Ok(await bluePrintService.RegisterForBluePrintAsync(bluePrint));
            }).WithTags(endpointGroup)
            .WithOpenApi();
        }

    }
}
