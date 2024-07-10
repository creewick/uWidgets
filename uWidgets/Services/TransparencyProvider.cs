using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace uWidgets.Services;

public class TransparencyProvider : MarkupExtension
{
    public string? Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var levels = Value?.Split(',')
            .Select(value => value.Trim())
            .Select(value => value switch
            {
                nameof(WindowTransparencyLevel.None) => WindowTransparencyLevel.None,
                nameof(WindowTransparencyLevel.Transparent) => WindowTransparencyLevel.Transparent,
                nameof(WindowTransparencyLevel.Blur) => WindowTransparencyLevel.Blur,
                nameof(WindowTransparencyLevel.AcrylicBlur) => WindowTransparencyLevel.AcrylicBlur,
                nameof(WindowTransparencyLevel.Mica) => WindowTransparencyLevel.Mica,
                _ => throw new ArgumentException($"Invalid transparency level: {value}")
            })
            .ToList() ?? [];

        return levels.AsReadOnly();
    }
}