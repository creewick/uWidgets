using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class WidgetLayoutProvider(ILayoutProvider layoutProvider, WidgetLayout widgetLayout) 
    : IWidgetLayoutProvider
{
    public event EventHandler<WidgetLayout>? DataChanging;
    public event EventHandler<WidgetLayout>? DataChanged;
    public WidgetLayout Get() => widgetLayout;

    public void Save(WidgetLayout data)
    {
        DataChanging?.Invoke(this, data);
        var layout = layoutProvider.Get();
        var index = layout.IndexOf(widgetLayout);

        if (index == -1) return;
        
        layout[index] = data;
        layoutProvider.Save(layout);
        widgetLayout = data;
        DataChanged?.Invoke(this, data);
    }

    public void Remove()
    {
        var layout = layoutProvider.Get();
        layout.Remove(widgetLayout);
        layoutProvider.Save(layout);
    }
}