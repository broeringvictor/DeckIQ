using DeckIQ.Core.Handlers;
using DeckIQ.Core.Requests.Accounts;
using DeckIQ.Core.Responses;
using DeckIQ.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DeckIQ.Web.Pages.Identity;

public abstract partial class LoginPage : ComponentBase
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

    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;
    public LoginRequest InputModel { get; set; } = new();

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
            else if (result.Message != null) Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}