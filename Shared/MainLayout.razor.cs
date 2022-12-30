using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace SmallTaskForceWeb.Shared;

public partial class MainLayout
{
    private MudTheme _theme = new();
    private bool _isDarkMode;
    private MudThemeProvider _mudThemeProvider;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (firstRender)
        //{
        //    _isDarkMode = await _mudThemeProvider.GetSystemPreference();
        //    StateHasChanged();
        //}
    }

    bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    string themModeText = "Switch to Dark Theme";
    async void OnThemeModeChange()
    {
        if(_isDarkMode)
        {
            _isDarkMode = false;
            themModeText = "Switch to Dark Theme";
            StateHasChanged();
        }
        else if(!_isDarkMode)
        {
            _isDarkMode = true;
            themModeText = "Switch to Light Theme";
            StateHasChanged();
        }
        
    }
}
