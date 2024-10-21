using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class DeleteFlashCardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("FlashCards: Delete")
            .WithSummary("Exclui um Flash Card")
            .WithDescription("Exclui uma Flash Card")
            .WithOrder(3)
            .Produces<Response<FlashCard?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFlashCardHandler handler,
        int id)
    {
        var request = new DeleteFlashCardRequest()
        {
            
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
    
}