using DeckIQ.Core.Models.OpenIa;
using DeckIQ.Core.Requests.OpenAi;
using DeckIQ.Core.Responses;

namespace DeckIQ.Core.Handlers;

public interface IOpenAiHandler
{
   Task<Response<OpenIaFlashCard?>> CreateAsync(CreateOpenAiFlashCardRequest request);
   
  
    


}