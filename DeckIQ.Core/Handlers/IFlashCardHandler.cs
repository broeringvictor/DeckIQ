using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Responses;
using DeckIQ.Core.Models;

namespace DeckIQ.Core.Handlers;

public interface IFlashCardHandler
{
    Task<Response<FlashCard?>> CreateAsync(CreateFlashCardRequest request);
    Task<Response<FlashCard?>> DeleteAsync(DeleteFlashCardRequest request);
    Task<Response<FlashCard?>> UpdateAsync(UpdateFlashCardRequest request);
    Task<Response<FlashCard?>> GetByIdAsync(GetFlashCardByIdRequest request);
    Task<PagedResponse<List<FlashCard>?>> GetByPeriodAsync(GetFlashCardSByPeriodRequest request);
    
    Task<Response<List<FlashCard>?>> GetRandomByCategoryAsync(GetRandomFlashCardsRequest request);
    Task<PagedResponse<List<FlashCard>?>> GetAllAsync(GetAllFlashCardsRequest request);

}