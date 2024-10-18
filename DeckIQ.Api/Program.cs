using System.Security.Claims;
using DeckIQ.Api.Data;
using DeckIQ.Api.EndPoints;
using DeckIQ.Api.Handlers;
using DeckIQ.Api.Models;
using DeckIQ.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

var cnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(cnnString); });

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<IFlashCardHandler, FlashCardHandler>();

// Crie o aplicativo após adicionar todos os serviços
var app = builder.Build();

// Configure o pipeline de requisição HTTP
app.UseRouting(); // Deve vir antes de UseAuthorization
app.UseAuthentication(); // Deve vir antes de UseAuthorization
app.UseAuthorization(); // Deve estar entre UseRouting e UseEndpoints

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/logout", async (SignInManager<User> signInManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet(pattern: "/roles", handler: (ClaimsPrincipal user) =>
    {
        if (!user.Identity!.IsAuthenticated || user.Identity is null)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Results.Json(roles);
    })
    .RequireAuthorization();

app.Run();

// endpoint → url de acesso 
// Convenção de Mercado
// Mesmo endpoint para todos os methods e no plural
// Versionamento v1 - v2