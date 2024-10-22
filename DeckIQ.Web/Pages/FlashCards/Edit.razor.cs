using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.FlashCards;

public partial class EditFlashCardPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public UpdateFlashCardRequest InputModel { get; set; } = new();
    protected List<Category> CategoryList { get; set; } = new(); // Adicionado para carregar categorias

    #endregion

    #region Parameters

    [Parameter]
    public int Id { get; set; }

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IFlashCardHandler Handler { get; set; } = null!;

    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!; // Adicionado para carregar categorias

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        // Carregar categorias
        await LoadCategoriesAsync();

        // Carregar FlashCard para edição
        GetFlashCardByIdRequest? request = new()
        {
            Id = Id
        };

        if (request is null)
        {
            Snackbar.Add("Parâmetro inválido", Severity.Error);
            return;
        }

        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSuccess: true, Data: not null })
            {
                InputModel = new UpdateFlashCardRequest
                {
                    Id = response.Data.Id,
                    Question = response.Data.Question,
                    Answer = response.Data.Answer,
                    IncorrectAnswerA = response.Data.IncorrectAnswerA,
                    IncorrectAnswerB = response.Data.IncorrectAnswerB,
                    IncorrectAnswerC = response.Data.IncorrectAnswerC,
                    IncorrectAnswerD = response.Data.IncorrectAnswerD,
                    CardImage = response.Data.CardImage,
                    CategoryId = response.Data.CategoryId
                };
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods

    // Método para carregar categorias
    private async Task LoadCategoriesAsync()
    {
        try
        {
            var response = await CategoryHandler.GetAllAsync(new GetAllCategoriesRequest());
            if (response.IsSuccess && response.Data != null)
            {
                CategoryList = response.Data;
            }
            else
            {
                Snackbar.Add("Não foi possível carregar as categorias", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    // Método para submissão do formulário de edição
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("FlashCard atualizado com sucesso", Severity.Success);
                NavigationManager.NavigateTo("/flashcards");
            }
            else if (result.Message != null)
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}
