namespace DeckIQ.Api.Models.OpenAi
{
    public class FlashCardsPrompts
    {
        private readonly string _question;
        private readonly string _answer;

        // Construtor da classe, recebe a pergunta e a resposta correta
        public FlashCardsPrompts(string question, string answer)
        {
            _question = question;
            _answer = answer;
        }

        // Propriedade que gera o prompt com base na pergunta e resposta correta
        public string IncorrectAnswerPrompt
        {
            get
            {
                return $"Você é responsável por gerar respostas para um flashcard. " +
                       $"A questão e a resposta correta serão fornecidas. Sua tarefa é gerar 4 respostas incorretas, " +
                       $"garantindo que essas respostas sejam plausíveis, mas incorretas. A saída deve seguir o seguinte formato:\n\n" +
                       $"Questão: {_question}\n" +
                       $"Resposta correta: {_answer}\n" +
                       $"Alternativas incorretas:\n" +
                       $"incorrectAnswerA: [Alternativa incorreta A]\n" +
                       $"incorrectAnswerB: [Alternativa incorreta B]\n" +
                       $"incorrectAnswerC: [Alternativa incorreta C]\n" +
                       $"incorrectAnswerD: [Alternativa incorreta D]\n\n" +
                       $"Ao criar as respostas incorretas, certifique-se de que elas estejam relacionadas à pergunta, " +
                       $"mas não sejam iguais à resposta correta. As respostas devem ser plausíveis para enganar um estudante, " +
                       $"mas ainda assim claramente incorretas.\n\n" +
                       $"Exemplo de uso:\n\n" +
                       $"Entrada:\n" +
                       $"Questão: \"Qual é a capital da França?\"\n" +
                       $"Resposta correta: \"Paris\"\n" +
                       $"Saída esperada:\n\n" +
                       $"Questão: \"Qual é a capital da França?\"\n" +
                       $"Resposta correta: \"Paris\"\n" +
                       $"Alternativas incorretas:\n" +
                       $"incorrectAnswerA: \"Londres\"\n" +
                       $"incorrectAnswerB: \"Berlim\"\n" +
                       $"incorrectAnswerC: \"Roma\"\n" +
                       $"incorrectAnswerD: \"Madri\"";
            }
        }
    }
}
