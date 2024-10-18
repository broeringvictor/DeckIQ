using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class UpdateFlashCardEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("FlashCards: Update")
            .WithSummary("Atualiza um Flash Card")
            .WithDescription("Atualiza um Flash Card")
            .WithOrder(2)
            .Produces<Response<FlashCard?>>(); // Atualize aqui para FlashCard

    private static async Task<IResult> HandleAsync(
        //ClaimsPrincipal user,
        IFlashCardHandler handler, // Alterado para IFlashCardHandler
        UpdateFlashCardRequest request, // Alterado para UpdateFlashCardRequest
        long id)
    {
        request.UserId = "victor@victorb";
        //request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = (int)id;

        var result = await handler.UpdateAsync(request); // Usando o método UpdateAsync para FlashCard
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}