namespace uWidgets.Core.Models.Settings;

/// <summary>
/// Dimensions of a widget.
/// </summary>
/// <param name="Size">Size of 1x1 widget in pixels</param>
/// <param name="Margin">Margin between widgets in pixels</param>
/// <param name="Radius">Widget's corner radius in pixels</param>
public record Dimensions(int Size, int Margin, int Radius);