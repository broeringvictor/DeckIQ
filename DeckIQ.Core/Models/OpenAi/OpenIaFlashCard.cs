namespace DeckIQ.Core.Models.OpenIa;

public class OpenIaFlashCard
{
    public int Id { get; set; }
    public string? UserId { get; set; } = string.Empty;
    public DateTime RequesDateTime { get; set; } = DateTime.Now;
    public long? OpenIaTokens { get; set; }
    public string LLM { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string IncorrectAnswerA { get; set; } = string.Empty;
    public string IncorrectAnswerB { get; set; } = string.Empty;
    public string IncorrectAnswerC { get; set; } = string.Empty;
    public string IncorrectAnswerD { get; set; } = string.Empty;
    
}