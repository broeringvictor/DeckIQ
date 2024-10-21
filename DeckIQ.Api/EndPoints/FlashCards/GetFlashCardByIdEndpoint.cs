using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class GetFlashCardByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/by-id/{id:long}", HandleAsync)

            .WithName("FlashCards: Get By Id")
            .WithSummary("Recupera um Flash Card")
            .WithDescription("Recupera um Flash Card")
            .WithOrder(4)
            .Produces<Response<FlashCard?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFlashCardHandler handler,
        int id)
    {
        var request = new GetFlashCardByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}