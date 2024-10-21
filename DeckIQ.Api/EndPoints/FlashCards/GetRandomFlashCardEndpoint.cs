using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class GetRandomFlashCardsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/random", Handle)
            .WithName("FlashCards: Get Random By Category")
            .WithSummary("Recupera flashcards aleatórios por categoria")
            .WithDescription("Recupera flashcards aleatórios por categoria")
            .WithOrder(6)
            .Produces<Response<List<FlashCard>?>>();

    private static async Task<IResult> Handle(
        ClaimsPrincipal user,
        [FromServices] IFlashCardHandler handler,
        [FromQuery] int categoryId,
        [FromQuery] int quantity = 5)
    {
        var request = new GetRandomFlashCardsRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            CategoryId = categoryId,
            Quantity = quantity
        };

        var result = await handler.GetRandomByCategoryAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
