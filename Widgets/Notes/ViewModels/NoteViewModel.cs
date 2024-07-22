using Avalonia;
using Notes.Models;
using ReactiveUI;

namespace Notes.ViewModels;

public class NoteViewModel(NoteModel noteModel) : ReactiveObject
{
    public string? Title => noteModel.Title;
    public string? Content => noteModel.Content;
    public string? Updated => noteModel.Updated.ToString();
    
    private Point lineEnd;

    public Point LineEnd
    {
        get => lineEnd;
        set => this.RaiseAndSetIfChanged(ref lineEnd, value);
    }
}