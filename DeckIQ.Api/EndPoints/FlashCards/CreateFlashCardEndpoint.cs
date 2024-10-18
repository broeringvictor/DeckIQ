using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class CreateFlashCardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria um novo flash card")
            .WithDescription("Cria um novo Flash card")
            .WithOrder(1)
            .Produces<Response<FlashCard?>>();

    private static async Task<IResult> HandleAsync(
        //ClaimsPrincipal user,
        IFlashCardHandler handler,
        CreateFlashCardRequest request)
    {
        //request.UserId = user.Identity?.Name ?? string.Empty;
        request.UserId = "victor@victorb";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
    }
}