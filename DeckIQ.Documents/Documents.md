# DeckIQ
## Property
#### Description: 
>Aplicativo voltado para a criação de FlashCards

## Structure
### DeckIQ.Api
#### Function: 
> BackEnd do projeto.
> Busca armazenar tudo aquilo que o cliente não pode acessar.
> Foi criado com um projeto simples Web 'dotnet create web -o DeckIQ'

### DeckIQ.Core
#### Function: 
> Objetivo é servir de ponto para o BackEnd e o Front.
> Muito dos códigos do back e do front são compartilhados, dessa forma ele serve para unificar-los.
>



### 1. Implementação do Identity API com Claims
Na API, foi utilizado o endpoint:

`endpoints.MapGroup("v1/identity")
.WithTags("Identity")
.MapIdentityApi<User>();`

Optamos pelos padrões do ASP.NET Identity, criando dois novos endpoints: roles (para gerenciar papéis) e logout (para finalizar a sessão). Isso exigiu adaptações no backend e frontend para utilizar o Claims-based Authentication.

Backend
Configuramos o ClaimsPrincipal para associar claims aos usuários autenticados, como permissões e roles:
csharp
Copiar código
````var claims = new List<Claim>
{
new Claim(ClaimTypes.Name, user.UserName),
new Claim(ClaimTypes.Role, "Administrator")
};
````
Frontend
Adaptação do login e controle de acesso para verificar claims e roles:
````c#
if (User.IsInRole("Administrator"))
{
// Exibir funcionalidades de administrador
}
````
