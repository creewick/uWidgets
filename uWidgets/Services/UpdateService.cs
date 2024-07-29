using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using uWidgets.Core.Interfaces;
using uWidgets.Views;

namespace uWidgets.Services;

public class UpdateService
{
    private readonly IAppSettingsProvider appSettingsProvider;
    private readonly HttpClient httpClient = new();

    public UpdateService(IAppSettingsProvider appSettingsProvider)
    {
        this.appSettingsProvider = appSettingsProvider;
        TimerService.Timer1Day.Subscribe(CheckForUpdates);
    }

    public void CheckForUpdates()
    {
        _ = CheckForUpdatesAsync();
    }
    
    public async Task CheckForUpdatesAsync()
    {
        var version = await GetUpdateVersionAsync();
        if (version == null) return;

        new UpdatePopup(version, this).Show();
    }
    
    public async Task<Version?> GetUpdateVersionAsync()
    {
        var response = await httpClient.GetAsync("https://github.com/creewick/uWidgets/releases/latest");

        if (!response.IsSuccessStatusCode) return null;

        var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
        var latestVersionText = response.RequestMessage?.RequestUri?.ToString().Split("/").Last().Replace("v", "") ?? "";

        if (!Version.TryParse(latestVersionText, out var latestVersion)) return null;

        var ignoreVersionText = appSettingsProvider.Get().IgnoreUpdate;

        if (ignoreVersionText != null && Version.TryParse(ignoreVersionText, out var ignoreVersion) &&
            latestVersion <= ignoreVersion)
            return null;

        if (latestVersion <= currentVersion) 
            return null;

        return latestVersion;
    }

    public void SkipVersion(Version version)
    {
        var settings = appSettingsProvider.Get() with { IgnoreUpdate = version.ToString() };
        appSettingsProvider.Save(settings);
    }
}