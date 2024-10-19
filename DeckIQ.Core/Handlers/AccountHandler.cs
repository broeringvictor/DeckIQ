using DeckIQ.Core.Requests.Accounts;
using DeckIQ.Core.Responses;

namespace DeckIQ.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<string?>> LoginAsync(LoginRequest loginRequest);
    Task<Response<string?>> RegisterAsync(RegisterRequest registerRequest);
    Task LogoutAsync();
}