using System.Net.Http.Json;
using DeckIQ.Core.Common.Extensions;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;

namespace DeckIQ.Web.Handlers;

public class FlashCardHandler(IHttpClientFactory httpClientFactory) : IFlashCardHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<FlashCard?>> CreateAsync(CreateFlashCardRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/flashcards", request).ConfigureAwait(false);
        return await result.Content.ReadFromJsonAsync<Response<FlashCard?>>().ConfigureAwait(false)
               ?? new Response<FlashCard?>(null, 400, "Não foi possível criar o flashcard");
    }

    public async Task<Response<FlashCard?>> UpdateAsync(UpdateFlashCardRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/flashcards/{request.Id}", request).ConfigureAwait(false);
        return await result.Content.ReadFromJsonAsync<Response<FlashCard?>>().ConfigureAwait(false)
               ?? new Response<FlashCard?>(null, 400, "Não foi possível atualizar o flashcard");
    }

    public async Task<Response<FlashCard?>> DeleteAsync(DeleteFlashCardRequest request)
    {
        var result = await _client.DeleteAsync($"v1/flashcards/{request.Id}").ConfigureAwait(false);
        return await result.Content.ReadFromJsonAsync<Response<FlashCard?>>().ConfigureAwait(false)
               ?? new Response<FlashCard?>(null, 400, "Não foi possível excluir o flashcard");
    }

    public async Task<Response<FlashCard?>> GetByIdAsync(GetFlashCardByIdRequest request)
    {
        // Atualizando a rota para /by-id/{id:long}
        var result = await _client.GetFromJsonAsync<Response<FlashCard?>>($"v1/flashcards/by-id/{request.Id}")
            .ConfigureAwait(false);
    
        return result ?? new Response<FlashCard?>(null, 400, "Não foi possível obter o flashcard");
    }

    public async Task<PagedResponse<List<FlashCard>?>> GetByPeriodAsync(GetFlashCardSByPeriodRequest request)
    {
        const string format = "yyyy-MM-dd";
        var startDate = request.StartDate?.ToString(format) ?? DateTime.Now.GetFristDay().ToString(format);
        var endDate = request.EndDate?.ToString(format) ?? DateTime.Now.GetLastDay().ToString(format);

        var url =
            $"v1/flashcards?startDate={startDate}&endDate={endDate}&pageNumber={request.PageNumber}&pageSize={request.PageSize}";

        var result = await _client.GetFromJsonAsync<PagedResponse<List<FlashCard>?>>(url).ConfigureAwait(false);
        return result ?? new PagedResponse<List<FlashCard>?>(null, 400, "Não foi possível obter os flashcards");
    }

    public async Task<Response<List<FlashCard>?>> GetRandomByCategoryAsync(GetRandomFlashCardsRequest request)
    {
        var url = $"v1/flashcards/random?categoryId={request.CategoryId}&quantity={request.Quantity}";
        var response = await _client.GetAsync(url).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<Response<List<FlashCard>?>>().ConfigureAwait(false)
               ?? new Response<List<FlashCard>?>(null, 500, "Não foi possível obter os flashcards aleatórios.");
    }

    public async Task<PagedResponse<List<FlashCard>?>> GetAllAsync(GetAllFlashCardsRequest request)
    {
        var result =
            await _client.GetAsync(
                $"v1/flashcards/all-by-category?categoryId={request.CategoryId}" +
                $"&pageNumber={request.PageNumber}&pageSize={request.PageSize}");
        return await result.Content.ReadFromJsonAsync<PagedResponse<List<FlashCard>>>()
               ?? new PagedResponse<List<FlashCard>>(null!, 400, "Não foi possível obter as categorias");
    }
}
    
    
        
