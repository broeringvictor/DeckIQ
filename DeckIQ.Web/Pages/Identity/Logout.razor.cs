using DeckIQ.Core.Handlers;
using DeckIQ.Web.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace DeckIQ.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IAccountHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            
            // Remover manualmente o cookie de autenticação no navegador
            await JsRuntime.InvokeVoidAsync("document.cookie",
                "auth-cookie=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/");

            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();

            // Redirecionar para a página de login ou página inicial
            NavigationManager.NavigateTo("/login", true);
        }

        await base.OnInitializedAsync();
    }

    #endregion
}
