using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

public class WidgetLayoutProvider(ILayoutProvider layoutProvider, WidgetLayout widgetLayout) 
    : IWidgetLayoutProvider
{
    public event DataChangedEvent<WidgetLayout>? DataChanging;
    public event DataChangedEvent<WidgetLayout>? DataChanged;
    public WidgetLayout Get() => widgetLayout;

    public void Save(WidgetLayout data)
    {
        DataChanging?.Invoke(this, widgetLayout, data);
        var layout = layoutProvider.Get();
        var index = layout.IndexOf(widgetLayout);

        if (index == -1) return;
        
        layout[index] = data;
        layoutProvider.Save(layout);
        var oldData = widgetLayout;
        widgetLayout = data;
        DataChanged?.Invoke(this, oldData, data);
    }

    public void Remove()
    {
        var layout = layoutProvider.Get();
        layout.Remove(widgetLayout);
        layoutProvider.Save(layout);
    }
}