namespace Notes.Models;

public record NoteModel(
    string? Title = null, 
    string? Content = null, 
    DateTime? Updated = null);