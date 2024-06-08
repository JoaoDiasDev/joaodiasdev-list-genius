using MudBlazor;

namespace ListGenius.Web.Util;

public static class CustomThemes
{
    public static readonly MudTheme CustomTheme = new()
    {
        PaletteDark = new PaletteDark()
        {
            Black = "#27272f",
            Background = "#32333d",
            BackgroundGrey = "#27272f",
            Surface = "#373740",
            DrawerBackground = "#27272f",
            DrawerText = "rgba(255,255,255, 0.50)",
            DrawerIcon = "rgba(255,255,255, 0.50)",
            AppbarBackground = "#27272f",
            AppbarText = "rgba(255,255,255, 0.70)",
            TextPrimary = "rgba(255,255,255, 0.70)",
            TextSecondary = "rgba(255,255,255, 0.50)",
            ActionDefault = "#adadb1",
            ActionDisabled = "rgba(255,255,255, 0.26)",
            ActionDisabledBackground = "rgba(255,255,255, 0.12)"
        },
        Palette = new PaletteLight()
        {
            Black = "#27272f",
            Background = "#f5f5f5",
            BackgroundGrey = "#e0e0e0",
            Surface = "#ffffff",
            DrawerBackground = "#ffffff",
            DrawerText = "rgba(0,0,0, 0.70)",
            DrawerIcon = "rgba(0,0,0, 0.70)",
            AppbarBackground = "#ffffff",
            AppbarText = "rgba(0,0,0, 0.87)",
            TextPrimary = "rgba(0,0,0, 0.87)",
            TextSecondary = "rgba(0,0,0, 0.54)",
            ActionDefault = "#6e6e6e",
            ActionDisabled = "rgba(0,0,0, 0.26)",
            ActionDisabledBackground = "rgba(0,0,0, 0.12)"
        },
        LayoutProperties = new LayoutProperties()
        {
            DrawerWidthLeft = "260px",
            DrawerWidthRight = "300px"
        }
    };

}