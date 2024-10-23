using System.ComponentModel.DataAnnotations;

namespace DeckIQ.Core.Requests.OpenAi;

public class CreateOpenAiFlashCardRequest : Request
{
    [Required(ErrorMessage = "O campo 'Questão' é obrigatório")]
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string Question { get; set; } = string.Empty;
    
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string Answer { get; set; } = string.Empty;
    
}