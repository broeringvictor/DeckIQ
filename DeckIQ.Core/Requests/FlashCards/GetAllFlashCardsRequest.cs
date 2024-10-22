namespace DeckIQ.Core.Requests.FlashCards;

public class GetAllFlashCardsRequest : PagedRequest
{
    public int CategoryId { get; set; }
    
}