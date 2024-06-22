using Clock.ViewModels;
using uWidgets;
using uWidgets.Core.Interfaces;

namespace Clock.Views;

public partial class AnalogIII : Widget
{
    public AnalogIII(IAppSettingsProvider appSettings, IWidgetSettingsProvider widgetSettings) 
        : base(widgetSettings)
    {
        DataContext = new AnalogClockViewModel(appSettings, widgetSettings);
        InitializeComponent();
    }
}