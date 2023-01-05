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
    private MudThemeProvider _mudThemeProvider;

    

    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    //if (firstRender)
    //    //{
    //    //    _isDarkMode = await _mudThemeProvider.GetSystemPreference();
    //    //    StateHasChanged();
    //    //}
    //}
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
                themModeText = "Switch to Light Theme";
                _appBarBackgroundColorCSS = "background-color:#27272fff";
                StateHasChanged();
            }
            else
            {
                _isDarkMode = false;
                themModeText = "Switch to Dark Theme";
                _appBarBackgroundColorCSS = "background-color:#fff";
                StateHasChanged();
            }
        }
    }

    bool _drawerOpen = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    string _appBarBackgroundColorCSS = "background-color:#fff";
    string themModeText = "Switch to Dark Theme";
    async void OnThemeModeChange()
    {
        if(_isDarkMode)
        {
            _isDarkMode = false;
            themModeText = "Switch to Dark Theme";
            _appBarBackgroundColorCSS = "background-color:#fff";
            StateHasChanged();
        }
        else if(!_isDarkMode)
        {
            _isDarkMode = true;
            themModeText = "Switch to Light Theme";
            _appBarBackgroundColorCSS = "background-color:#27272fff";
            StateHasChanged();
        }
        await LocalStorage.SetItemAsStringAsync("_isDarkMode", _isDarkMode.ToString());
    }
}
