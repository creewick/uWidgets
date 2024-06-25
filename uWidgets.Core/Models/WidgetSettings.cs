using System.Text.Json;

namespace uWidgets.Core.Models;

public record WidgetSettings(string Type, string SubType, int X, int Y, int Width, int Height, JsonElement? Model);