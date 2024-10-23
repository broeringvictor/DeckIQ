using DeckIQ.Api;
using DeckIQ.Api.Common.Api;
using DeckIQ.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Carregando os segredos de usuário
builder.Configuration.AddUserSecrets<Program>();

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocs();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseSecurity();
app.UseRouting();
app.UseCors(ApiConfiguration.CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();