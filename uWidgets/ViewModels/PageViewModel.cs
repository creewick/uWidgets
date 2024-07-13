using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace uWidgets.ViewModels;

public record PageViewModel(Type? Type, string IconName, string Text)
{
    public StreamGeometry? Icon => (StreamGeometry?) Application.Current?.FindResource(IconName);
};