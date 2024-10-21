using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.FlashCards;
using DeckIQ.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.FlashCards
{
    public partial class StudyFlashCardsPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<FlashCard> FlashCards { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int SelectedCategoryId { get; set; } = 0;
        public int Quantity { get; set; } = 5;

        // Dicionário para manter o estado de cada flashcard
        protected Dictionary<int, FlashCardState> FlashCardStates { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public IFlashCardHandler FlashCardHandler { get; set; } = null!;

        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

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
                var result = await CategoryHandler.GetAllAsync(request);
                if (result.IsSuccess && result.Data != null)
                    Categories = result.Data;
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

        public async Task LoadFlashCardsAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetRandomFlashCardsRequest
                {
                    CategoryId = SelectedCategoryId,
                    Quantity = Quantity,
                    UserId = "user-id" // Substitua pelo ID do usuário atual
                };

                var result = await FlashCardHandler.GetRandomByCategoryAsync(request);

                if (result.IsSuccess && result.Data != null)
                {
                    FlashCards = result.Data;
                    InitializeFlashCardStates();
                    StateHasChanged(); // Força atualização da UI
                    Snackbar.Add($"Total de FlashCards carregados: {FlashCards.Count}", Severity.Info);
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao carregar flashcards", Severity.Error);
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

        private void InitializeFlashCardStates()
        {
            FlashCardStates.Clear();
            foreach (var flashCard in FlashCards)
            {
                FlashCardStates[flashCard.Id] = new FlashCardState();
                ShowOptions(flashCard); // Garante que as opções serão exibidas
            }
        }

        public void ShowOptions(FlashCard flashCard)
        {
            var state = FlashCardStates[flashCard.Id];
            state.ShowOptions = true;

            // Adicionar opções e embaralhá-las
            state.Options = new List<string>
                {
                    flashCard.Answer,
                    flashCard.IncorrectAnswerA,
                    flashCard.IncorrectAnswerB,
                    flashCard.IncorrectAnswerC,
                    flashCard.IncorrectAnswerD
                }
                .Where(o => !string.IsNullOrWhiteSpace(o)) // Filtrar respostas vazias
                .OrderBy(x => Guid.NewGuid()) // Embaralhar as opções
                .ToList();
        }


        public void CheckAnswer(FlashCard flashCard)
        {
            var state = FlashCardStates[flashCard.Id];

            // Normalizar ambas as respostas para evitar problemas com espaços e maiúsculas/minúsculas
            var selectedOptionNormalized = state.SelectedOption?.Trim().ToLower();
            var correctAnswerNormalized = flashCard.Answer?.Trim().ToLower();

            // Depuração: imprimir as respostas para verificar
            Console.WriteLine($"Selected Option: {selectedOptionNormalized}, Correct Answer: {correctAnswerNormalized}");

            // Comparar respostas normalizadas
            state.IsCorrect = selectedOptionNormalized == correctAnswerNormalized;

            // Forçar a atualização da interface
            StateHasChanged();
        }


        #endregion

        #region Nested Classes

        public class FlashCardState
        {
            public string UserAnswer { get; set; } = string.Empty;
            public bool ShowOptions { get; set; } = false;
            public List<string> Options { get; set; } = new();
            public string SelectedOption { get; set; } = string.Empty;
            public bool? IsCorrect { get; set; } = null;
        }

        #endregion
    }
}
