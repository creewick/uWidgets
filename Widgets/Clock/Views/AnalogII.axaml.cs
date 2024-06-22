using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Clock.ViewModels;
using uWidgets;
using uWidgets.Core.Interfaces;

namespace Clock.Views;

public partial class AnalogII : Widget
{
    public AnalogII(IAppSettingsProvider appSettings, IWidgetSettingsProvider widgetSettings) 
        : base(widgetSettings)
    {
        DataContext = new AnalogClockViewModel(appSettings, widgetSettings);
        InitializeComponent();
    }
}