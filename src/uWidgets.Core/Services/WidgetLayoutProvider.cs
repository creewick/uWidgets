using uWidgets.Core.Interfaces;
using uWidgets.Core.Models;

namespace uWidgets.Core.Services;

/// <inheritdoc />
public class WidgetLayoutProvider(ILayoutProvider layoutProvider, WidgetLayout widgetLayout) : IWidgetLayoutProvider
{
    /// <inheritdoc />
    public event DataChangedEvent<WidgetLayout>? DataChanging;

    /// <inheritdoc />
    public event DataChangedEvent<WidgetLayout>? DataChanged;

    /// <inheritdoc />
    public WidgetLayout Get() => widgetLayout;

    /// <inheritdoc />
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

    /// <inheritdoc />
    public void Remove()
    {
        var layout = layoutProvider.Get();
        layout.Remove(widgetLayout);
        layoutProvider.Save(layout);
    }
}