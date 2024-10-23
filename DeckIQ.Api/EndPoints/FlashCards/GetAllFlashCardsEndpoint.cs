using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Configuration = DeckIQ.Core.Configuration;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class GetAllFlashCardsEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/all-by-category", HandleAsync)  // Alterado para GET, já que estamos realizando uma busca
            .WithName("FlashCards: Busca todos os FlashCards de uma categoria")
            .WithSummary("Busca todos os FlashCards de uma categoria")
            .WithDescription("Busca Todos os FlashCards de uma categoria")
            .WithOrder(5)
            .Produces<PagedResponse<List<FlashCard>>>();

   
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFlashCardHandler handler,
        [FromQuery] int categoryId,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        
        if (string.IsNullOrEmpty(user.Identity?.Name))
        {
            return TypedResults.Unauthorized(); 
        }

       
        var request = new GetAllFlashCardsRequest
        {
            UserId = user.Identity?.Name ?? string.Empty, // Pega o nome do usuário autenticado
            CategoryId = categoryId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        
        var result = await handler.GetAllAsync(request);
        
        
        return result.IsSuccess
            ? TypedResults.Ok(result) 
            : TypedResults.BadRequest(result); 
    }
}
