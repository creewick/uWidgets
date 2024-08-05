using Avalonia.Controls;
using Avalonia.Interactivity;
using uWidgets.Core.Interfaces;
using Weather.Models.Geocoding;
using Weather.Services;
using Weather.ViewModels;

namespace Weather.Views.Settings;

public partial class ForecastSettings : UserControl
{
    private readonly OpenMeteoWeatherProvider provider;
    
    public ForecastSettings(IWidgetLayoutProvider widgetLayoutProvider)
    {
        provider = new OpenMeteoWeatherProvider();
        DataContext = new ForecastSettingsViewModel(widgetLayoutProvider);
        InitializeComponent();
        Search.AsyncPopulator = SearchCity;
    }

    private async Task<IEnumerable<object>> SearchCity(string? query, CancellationToken token)
    {
        return (await provider.GetCitiesAsync(query ?? "") ?? []);
    }
    
    private void Search_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not ForecastSettingsViewModel viewModel || Search.SelectedItem is not City city) return;
        viewModel.Location = city;
    }
}