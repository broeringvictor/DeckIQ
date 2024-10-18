using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class GetFlashCardByPeriodEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("FlashCards: Get By Period")
            .WithSummary("Recupera Flash Cards por período")
            .WithDescription("Recupera Flash Cards por período")
            .WithOrder(5)
            .Produces<PagedResponse<List<FlashCard?>?>>(); // Produz uma resposta paginada com uma lista de FlashCards

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFlashCardHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetFlashCardSByPeriodRequest // Ajustado para GetFlashCardSByPeriodRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriod(request); // Chama o método GetByPeriod do handler
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}