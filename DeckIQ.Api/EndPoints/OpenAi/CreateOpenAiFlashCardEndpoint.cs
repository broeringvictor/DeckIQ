using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Api.Handlers;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models.OpenIa;
using DeckIQ.Core.Requests.OpenAi;
using DeckIQ.Core.Responses;

namespace DeckIQ.Api.EndPoints.OpenAi
{
    public class CreateOpenAiFlashCardEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost($"/flashcard", HandleAsync)
                .WithName("OpenIa: Criar quatro respostas erradas")
                .WithSummary("Produz quatro respostas falsas a partir de uma questão.")
                .WithDescription("Recebe a questão e a resposta de um flashcard e produz quatro respostas falsas.")
                .WithOrder(1)
                .Produces<Response<OpenIaFlashCard?>>()
                .Accepts<CreateOpenAiFlashCardRequest>("application/json");
        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            CreateOpenAiFlashCardRequest request,
            IOpenAiHandler handler)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;

            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result.Data);
        }
        
    }
}