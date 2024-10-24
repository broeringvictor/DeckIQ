﻿using System.ComponentModel.DataAnnotations;

namespace DeckIQ.Core.Requests.FlashCards;

public class UpdateFlashCardRequest : Request
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo 'Questão' é obrigatório")]
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string Question { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O campo 'Resposta' é obrigatório")]
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string Answer { get; set; } = string.Empty;
    
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string IncorrectAnswerA { get; set; } = string.Empty;
    
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string IncorrectAnswerB { get; set; } = string.Empty;
    
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string IncorrectAnswerC { get; set; } = string.Empty;
    
    [MaxLength(300, ErrorMessage = "O campo deve conter no maxímo 300 caracteres")]
    public string IncorrectAnswerD { get; set; } = string.Empty;
    
    public string? CardImage { get; set; } 
    
    public int CategoryId { get; set; }
    

}