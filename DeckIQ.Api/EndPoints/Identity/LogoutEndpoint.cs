using DeckIQ.Api.Common.Api;
using DeckIQ.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace DeckIQ.Api.EndPoints.Identity;

public class LogoutEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization();

    private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}