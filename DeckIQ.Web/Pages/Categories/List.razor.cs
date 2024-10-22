using System.Diagnostics;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = new();
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        await LoadCategoriesAsync();
    }

    #endregion

    #region Methods

    private async Task LoadCategoriesAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Categories = result.Data ?? new List<Category>();
            else
                Snackbar.Add(result.Message ?? "Erro ao carregar categorias", Severity.Error);
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

    public async Task OnDeleteButtonClickedAsync(int id, string? title)
    {
        bool? result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir, a categoria '{title}' será excluída. Esta é uma ação irreversível! Deseja continuar?",
            yesText: "EXCLUIR",
            cancelText: "Cancelar");

        if (result == true)
        {
            await OnDeleteAsync(id, title);
        }
    }

    private async Task OnDeleteAsync(int id, string? title)
    {
        try
        {
            var request = new DeleteCategoryRequest { Id = id };
            var response = await Handler.DeleteAsync(request);
            if (response.IsSuccess)
            {
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria '{title}' excluída", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Message ?? "Erro ao excluir a categoria", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public bool Filter(Category category)
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        Debug.Assert(category.Title != null, "category.Title != null");
        if (category.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (!string.IsNullOrEmpty(category.Description) &&
            category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    #endregion
}
