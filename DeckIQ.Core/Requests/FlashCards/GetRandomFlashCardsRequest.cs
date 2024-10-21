namespace DeckIQ.Core.Requests.FlashCards;

public class GetRandomFlashCardsRequest : Request
{
    public int CategoryId { get; set; }
    public int Quantity { get; set; } = 5; // Padrão para 5 flashcards
}
