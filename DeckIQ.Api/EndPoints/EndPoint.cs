using DeckIQ.Api.Common.Api;
using DeckIQ.Api.EndPoints.Categories;
using DeckIQ.Api.EndPoints.FlashCards;
using DeckIQ.Api.EndPoints.Identity;
using DeckIQ.Api.Models;

namespace DeckIQ.Api.EndPoints;

public static class EndPoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });
        
        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/flashcards")
            .WithTags("FlashCards")
            .RequireAuthorization()
            .MapEndpoint<CreateFlashCardEndpoint>()
            .MapEndpoint<UpdateFlashCardEndpoint>()
            .MapEndpoint<DeleteFlashCardEndpoint>()
            .MapEndpoint<GetRandomFlashCardsEndpoint>() // Coloque antes
            .MapEndpoint<GetFlashCardByIdEndpoint>()    // Coloque depois
            .MapEndpoint<GetFlashCardByPeriodEndpoint>();



        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();
            
        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();
        
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
    
    
}