using System.Net.Http.Json;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models.OpenIa;
using DeckIQ.Core.Requests.OpenAi;
using DeckIQ.Core.Responses;

namespace DeckIQ.Web.Handlers;

public class OpenAiHandler (IHttpClientFactory httpClientFactory) : IOpenAiHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<OpenIaFlashCard?>> CreateAsync(CreateOpenAiFlashCardRequest request)
    {
        var result = await _client.PostAsJsonAsync("/v1/openai/flashcard", request).ConfigureAwait(false);
        return await result.Content.ReadFromJsonAsync<Response<OpenIaFlashCard?>>().ConfigureAwait(false)
               ?? new Response<OpenIaFlashCard?>(null, 400, "Não foi possível criar as respostas incorretas.");
    }
}