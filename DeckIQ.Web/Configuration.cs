using MudBlazor;

namespace DeckIQ.Web;

public static class Configuration
{
    public const string HttpClientName = "deckiq";
    public static string BackendUrl { get; set; } = "https://localhost:7283/";

    public static readonly MudTheme ThemeHeadless = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#365f7a",
            Secondary = "#4d4d4d",
            Background = Colors.Gray.Lighten4,
            AppbarBackground = "#808080",
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.Black,
            // Defina outras cores conforme necessário
        },
        // Você pode adicionar outras configurações como Typography, LayoutProperties, etc.
    };
}