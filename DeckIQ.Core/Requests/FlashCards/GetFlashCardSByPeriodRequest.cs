namespace DeckIQ.Core.Requests.FlashCards;

public class GetFlashCardSByPeriodRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}