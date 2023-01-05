using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;

namespace SmallTaskForceWeb.Shared;

public partial class MainLayout
{
    private MudTheme _theme = new();
    private bool _isDarkMode;
    private MudThemeProvider? _mudThemeProvider;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // set them mode from system preference
        if (firstRender && string.IsNullOrEmpty(LocalStorage.ContainKeyAsync("_isDarkMode").ToString()))
        {
            _isDarkMode = await _mudThemeProvider.GetSystemPreference();
            StateChanged();
        }
    }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (await LocalStorage.ContainKeyAsync("_isDarkMode"))
        {
            string status = await LocalStorage.GetItemAsStringAsync("_isDarkMode");
            if(status != null && status == "True")
            {
                _isDarkMode = true;
                StateChanged();
            }
            else
            {
                _isDarkMode = false;
                StateChanged();
            }
        }
    }

    bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    string? _appBarBackgroundColorCSS;
    string themModeText = "Switch to Dark Theme";
    async void OnThemeModeChange()
    {
        if(_isDarkMode)
        {
            _isDarkMode = false;
            StateChanged();
        }
        else if(!_isDarkMode)
        {
            _isDarkMode = true;
            StateChanged();
        }
        await LocalStorage.SetItemAsStringAsync("_isDarkMode", _isDarkMode.ToString());
    }

    void StateChanged()
    {
        if (_isDarkMode)
        {   
            themModeText = "Switch to Light Theme";
            _appBarBackgroundColorCSS = "background-color:#27272fff";
            StateHasChanged();
        }
        else if (!_isDarkMode)
        {
            themModeText = "Switch to Dark Theme";
            _appBarBackgroundColorCSS = "background-color:#fff";
            StateHasChanged();
        }
    }
}
