using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using uWidgets.Core.Models;

namespace uWidgets.ViewModels;

public record PageViewModel(Type? Type, string IconName, string Text, AssemblyInfo? AssemblyInfo = null)
{
    public StreamGeometry? Icon => (StreamGeometry?) (Application.Current!.TryFindResource(IconName, out var icon) ? icon : null);
};