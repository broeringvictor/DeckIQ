using System.ComponentModel.DataAnnotations;

namespace DeckIQ.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Titulo inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter no maxímo 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}