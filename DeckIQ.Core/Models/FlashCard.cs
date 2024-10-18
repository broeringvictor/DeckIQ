namespace DeckIQ.Core.Models;

public class FlashCard
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string IncorrectAnswerA { get; set; } = string.Empty;
    public string IncorrectAnswerB { get; set; } = string.Empty;
    public string IncorrectAnswerC { get; set; } = string.Empty;
    public string IncorrectAnswerD { get; set; } = string.Empty;
    public string? CardImage { get; set; } 
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    
}