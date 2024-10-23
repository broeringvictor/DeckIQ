using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using DeckIQ.Core;
using Configuration = DeckIQ.Core.Configuration;

namespace DeckIQ.Api.EndPoints.FlashCards;

public class GetFlashCardByPeriodEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", Handle)
            .WithName("FlashCards: Get By Period")
            .WithSummary("Recupera Flash Cards por período")
            .WithDescription("Recupera Flash Cards por período")
            .WithOrder(5)
            .Produces<PagedResponse<List<FlashCard?>?>>(); // Produz uma resposta paginada com uma lista de FlashCards

    private static IResult Handle(
        ClaimsPrincipal user,
        IFlashCardHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageSize,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetFlashCardSByPeriodRequest // Ajustado para GetFlashCardSByPeriodRequest
            ();
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.PageNumber = pageNumber;
        request.PageSize = pageSize;
        request.StartDate = startDate;
        request.EndDate = endDate;

        var result = handler.GetByPeriodAsync(request).Result; // Chama o método GetByPeriod do handler
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}