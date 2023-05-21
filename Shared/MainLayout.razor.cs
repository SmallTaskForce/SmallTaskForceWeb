using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;

namespace SmallTaskForceWeb.Shared;

public partial class MainLayout
{
    private bool _isDarkMode;
    private MudThemeProvider? _mudThemeProvider;


    // https://mudblazor.com/features/colors#material-colors-list-of-material-colors
    MudTheme _theme = new MudTheme()
    {
        Palette = new Palette()
        {
            Primary = Colors.Blue.Default,
            Secondary = Colors.Blue.Accent2,
            AppbarBackground = Colors.Blue.Accent4,
        },
        PaletteDark = new PaletteDark()
        {
            Primary = Colors.Blue.Darken4,
        },

        //LayoutProperties = new LayoutProperties()
        //{
        //    DrawerWidthLeft = "260px",
        //    DrawerWidthRight = "300px"
        //}
    };

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
