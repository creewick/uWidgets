using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Notes.Models;
using Notes.ViewModels;
using uWidgets.Core.Interfaces;

namespace Notes.Views;

public partial class Note : UserControl
{
    private readonly NoteModel noteModel;
    private readonly IWidgetLayoutProvider widgetLayoutProvider;
    
    public Note(IWidgetLayoutProvider widgetLayoutProvider) : this(new NoteModel(), widgetLayoutProvider) {}
    
    public Note(NoteModel noteModel, IWidgetLayoutProvider widgetLayoutProvider)
    {
        this.noteModel = noteModel;
        this.widgetLayoutProvider = widgetLayoutProvider;
        DataContext = new NoteViewModel(noteModel);
        InitializeComponent();
    }

    private void UpdateContent(object? sender, RoutedEventArgs e)
    {
        var newText = (sender as TextBox)!.Text;

        var newModel = noteModel with { Content = newText, Updated = DateTime.Now };
        var newSettings = JsonSerializer.SerializeToElement(newModel);
        var newLayout = widgetLayoutProvider.Get() with { Settings = newSettings };
        
        widgetLayoutProvider.Save(newLayout);
    }
    
    private void UpdateTitle(object? sender, RoutedEventArgs e)
    {
        var newText = (sender as TextBox)!.Text;

        var newModel = noteModel with { Title = newText, Updated = DateTime.Now };
        var newSettings = JsonSerializer.SerializeToElement(newModel);
        var newLayout = widgetLayoutProvider.Get() with { Settings = newSettings };
        
        widgetLayoutProvider.Save(newLayout);
    }
}