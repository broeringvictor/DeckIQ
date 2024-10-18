using DeckIQ.Api.Data;
using DeckIQ.Api.EndPoints;
using DeckIQ.Api.Handlers;
using DeckIQ.Core.Handlers;
using DeckIQ.Core.Models;
using DeckIQ.Core.Requests.Categories;
using DeckIQ.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeckIQ.Api;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });

        var cnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(cnnString);
        });

        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();
        
        app.MapEndpoints();
        
        
        app.Run();
    }
}

// endpoint → url de acesso 
// Convenção de Mercado
// Mesmo endpoint para todos os methods e no plural
// Versionamento v1 - v2