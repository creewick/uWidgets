using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Reminders.Models;
using uWidgets.Core.Interfaces;

namespace Reminders.Views.Settings;

public partial class ListSettings : UserControl
{
    private readonly IWidgetLayoutProvider widgetLayoutProvider;

    public ListSettings(IWidgetLayoutProvider widgetLayoutProvider)
    {
        this.widgetLayoutProvider = widgetLayoutProvider;
        InitializeComponent();
    }

    private void DeleteCompleted(object? sender, RoutedEventArgs e)
    {
        var layout = widgetLayoutProvider.Get();
        var model = layout.GetModel<RemindersListModel>()!;
        model = model with { Reminders = model.Reminders
            .Where(entry => !entry.Completed)
            .ToList() };

        layout = layout with { Settings = JsonSerializer.SerializeToElement(model) };
        widgetLayoutProvider.Save(layout);
    }

    private void DeleteAll(object? sender, RoutedEventArgs e)
    {
        var layout = widgetLayoutProvider.Get();
        var model = layout.GetModel<RemindersListModel>()!;
        model = model with { Reminders = [] };

        layout = layout with { Settings = JsonSerializer.SerializeToElement(model) };
        widgetLayoutProvider.Save(layout);
    }
}