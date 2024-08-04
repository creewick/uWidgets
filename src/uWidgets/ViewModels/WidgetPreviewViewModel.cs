using Avalonia.Controls;

namespace uWidgets.ViewModels;

public record WidgetPreviewViewModel(UserControl Control, string Type, string Subtype, string? Title, string? Subtitle);