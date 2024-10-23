using System.Security.Claims;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;

using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Requests.OpenAi;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;


namespace DeckIQ.Web.Pages.FlashCards
{
    public partial class CreateFlashCardPage : ComponentBase
    {
        #region Properties
        
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationState { get; set; }
        public bool IsBusy { get; set; } = false;
        public CreateFlashCardRequest InputModel { get; set; } = new();

        // Lista de categorias
        protected List<Category> CategoryList { get; set; } = new();

        #endregion

        #region Services

        [Inject] public IFlashCardHandler Handler { get; set; } = null!;

        [Inject] public ICategoryHandler CategoryHandler { get; set; } = null!;

        [Inject] public IOpenAiHandler OpenAiHandler { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        [Inject] public IDialogService DialogService { get; set; } = null!;

        

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

        public async Task GenerateIncorrectAnswers()
        {
            bool? result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                "Tem certeza de que deseja gerar quatro respostas incorretas para esta questão?",
                yesText: "GERAR",
                cancelText: "Cancelar"
            );

            if (result == true)
            {
                IsBusy = true;
                try
                {
                    var request = new CreateOpenAiFlashCardRequest
                    {
                        Question = InputModel.Question,
                        Answer = InputModel.Answer
                    };

                    var response = await OpenAiHandler.CreateAsync(request);

                    if (response.IsSuccess && response.Data != null)
                    {
                        InputModel.IncorrectAnswerA = response.Data.IncorrectAnswerA;
                        InputModel.IncorrectAnswerB = response.Data.IncorrectAnswerB;
                        InputModel.IncorrectAnswerC = response.Data.IncorrectAnswerC;
                        InputModel.IncorrectAnswerD = response.Data.IncorrectAnswerD;
                        Snackbar.Add("Respostas incorretas geradas com sucesso.", Severity.Success);
                    }
                    else
                    {
                        Snackbar.Add(response.Message ?? "Erro ao gerar respostas incorretas.", Severity.Error);
                    }
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Erro: {ex.Message}", Severity.Error);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }


        #endregion
    }
}