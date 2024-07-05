using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace uWidgets.Models;

public record ListItemTemplate(Type? Type, string Text)
{
    public StreamGeometry? Icon => (StreamGeometry?) Application.Current?.FindResource(Text);
};