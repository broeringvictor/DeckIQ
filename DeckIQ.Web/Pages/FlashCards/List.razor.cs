using System.Globalization;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.FlashCards;

public partial class ListFlashCardPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<FlashCard> FlashCards { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string SearchTerm { get; set; } = string.Empty;
    public int SelectedCategoryId { get; set; }

    #endregion

    #region Services

    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public IFlashCardHandler Handler { get; set; } = null!;
    [Inject] public ICategoryHandler CategoryHandler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        await LoadCategoriesAsync();

        // Se houver categorias, seleciona a primeira automaticamente e carrega os flashcards
        if (Categories.Any())
        {
            await LoadFlashCardsAsync();
        }
    }

    #endregion

    #region Methods

    private async Task LoadCategoriesAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await CategoryHandler.GetAllAsync(request);

            if (result.IsSuccess && result.Data != null)
            {
                Categories = result.Data;
                Snackbar.Add("Categorias carregadas com sucesso", Severity.Success);
            }
            else
            {
                Snackbar.Add(result.Message ?? "Erro ao carregar categorias", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }
    }

    public async Task LoadFlashCardsAsync()
    {
        if (SelectedCategoryId == 0)
        {
            Snackbar.Add("Selecione uma categoria para carregar os FlashCards.", Severity.Warning);
            return;
        }

        IsBusy = true;
        try
        {
            var request = new GetAllFlashCardsRequest
            {
                CategoryId = SelectedCategoryId,
                PageNumber = 1, // Padrão, pode ser configurável
                PageSize = 20   // Padrão, pode ser configurável
            };

            var result = await Handler.GetAllAsync(request);

            if (result.IsSuccess && result.Data != null)
            {
                FlashCards = result.Data;
                Snackbar.Add("FlashCards carregados com sucesso", Severity.Success);
            }
            else
            {
                Snackbar.Add(result.Message ?? "Erro ao carregar FlashCards", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Erro ao carregar FlashCards: {ex.Message}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
            StateHasChanged();
        }
    }

    public async Task OnDeleteButtonClickedAsync(int id, string? title)
    {
        bool? result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir, o FlashCard '{title}' será excluído. Esta é uma ação irreversível! Deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "Cancelar"
        );

        if (result == true)
        {
            await OnDeleteAsync(id, title);
        }
    }

    private async Task OnDeleteAsync(int id, string? title)
    {
        try
        {
            var request = new DeleteFlashCardRequest { Id = id };
            var response = await Handler.DeleteAsync(request);

            if (response.IsSuccess)
            {
                FlashCards.RemoveAll(x => x.Id == id); 
                Snackbar.Add($"FlashCard '{title}' excluído com sucesso", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Message ?? "Erro ao excluir o FlashCard", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Erro ao excluir FlashCard: {ex.Message}", Severity.Error);
        }
    }

    public bool Filter(FlashCard flashCard)
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        return flashCard.Question.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
            || flashCard.Answer.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
            || flashCard.CreateDate.ToString(CultureInfo.CurrentCulture).Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
            || flashCard.LastUpdateDate.ToString(CultureInfo.CurrentCulture).Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
    }

    #endregion
}
