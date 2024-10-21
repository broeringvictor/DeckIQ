using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.FlashCards
{
    public partial class CreateFlashCardPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateFlashCardRequest InputModel { get; set; } = new();

        // Lista de categorias
        protected List<Category> CategoryList { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public IFlashCardHandler Handler { get; set; } = null!;

        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            // Obter a lista de categorias ao inicializar a página
            var response = await CategoryHandler.GetAllAsync(new GetAllCategoriesRequest());

            if (response.IsSuccess && response.Data != null)
            {
                CategoryList = response.Data;
            }
            else
            {
                Snackbar.Add("Não foi possível obter a lista de categorias.", Severity.Error);
            }
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    if (result.Message != null)
                        Snackbar.Add(result.Message, Severity.Success);
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
}
