using DeckIQ.Api.Data;
using DeckIQ.Api.Models.OpenAi;
using DeckIQ.Core;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models.OpenIa;
using DeckIQ.Core.Requests.OpenAi;
using DeckIQ.Core.Responses;
using OpenAI.Chat;

namespace DeckIQ.Api.Handlers
{
    public class OpenAiHandler : IOpenAiHandler
    {
        string llm = "gpt-4o-mini";
        private readonly AppDbContext _context;
        private readonly ChatClient _client;

        public OpenAiHandler(AppDbContext context)
        {
            _context = context;
            _client = new ChatClient(model: llm, Configuration.OpenAi);
        }

        public async Task<Response<OpenIaFlashCard?>> CreateAsync(CreateOpenAiFlashCardRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var prompt = new FlashCardsPrompts(request.Question, request.Answer).IncorrectAnswerPrompt;
                var chatCompletion = await _client.CompleteChatAsync(
                    new UserChatMessage(prompt));

                var incorrectAnswers = ExtractIncorrectAnswers(chatCompletion);

                var flashCard = new OpenIaFlashCard
                {
                    UserId = request.UserId, // UserId do usuário logado
                    Question = request.Question,
                    Answer = request.Answer,
                    IncorrectAnswerA = incorrectAnswers[0],
                    IncorrectAnswerB = incorrectAnswers[1],
                    IncorrectAnswerC = incorrectAnswers[2],
                    IncorrectAnswerD = incorrectAnswers[3],
                    LLM = llm,
                    RequesDateTime = DateTime.UtcNow,
                };

                await _context.OpenAiFlashCards.AddAsync(flashCard);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new Response<OpenIaFlashCard?>(flashCard, 201, "Respostas incorretas criadas com sucesso.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new Response<OpenIaFlashCard?>(null, 500, $"Erro ao criar o FlashCard: {ex.Message}");
            }
        }

        private List<string> ExtractIncorrectAnswers(ChatCompletion chatCompletion)
        {
            string? choices = chatCompletion.Content.FirstOrDefault()?.Text;
            var incorrectAnswers = new List<string>();

            if (!string.IsNullOrEmpty(choices))
            {
                var lines = choices.Split('\n');
                foreach (var line in lines)
                {
                    if (line.StartsWith("incorrectAnswerA:"))
                        incorrectAnswers.Add(line.Replace("incorrectAnswerA:", "").Trim());
                    if (line.StartsWith("incorrectAnswerB:"))
                        incorrectAnswers.Add(line.Replace("incorrectAnswerB:", "").Trim());
                    if (line.StartsWith("incorrectAnswerC:"))
                        incorrectAnswers.Add(line.Replace("incorrectAnswerC:", "").Trim());
                    if (line.StartsWith("incorrectAnswerD:"))
                        incorrectAnswers.Add(line.Replace("incorrectAnswerD:", "").Trim());
                }
            }

            return incorrectAnswers;
        }
    }
}