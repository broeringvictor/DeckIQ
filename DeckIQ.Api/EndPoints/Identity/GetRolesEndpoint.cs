using System.Security.Claims;
using DeckIQ.Api.Common.Api;
using DeckIQ.Core.Models.Accounts;

namespace DeckIQ.Api.EndPoints.Identity;

public class GetRolesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapGet("/roles", Handle)
            .RequireAuthorization();

    private static IResult Handle(ClaimsPrincipal user)
    {
        if (!user.Identity!.IsAuthenticated || user.Identity is null)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            });

        return Results.Json(roles);
    }
}