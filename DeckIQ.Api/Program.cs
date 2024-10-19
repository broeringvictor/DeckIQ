using DeckIQ.Api;
using DeckIQ.Api.Common.Api;
using DeckIQ.Api.EndPoints;



var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocs();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

// Adicione UseRouting se ainda não estiver presente
app.UseRouting();

// Mova o UseCors para logo após o UseRouting
app.UseCors(ApiConfiguration.CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();


// endpoint → url de acesso 
// Convenção de Mercado
// Mesmo endpoint para todos os methods e no plural
// Versionamento v1 - v2