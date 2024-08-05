using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace uWidgets.Views.Controls;

public partial class Setting : UserControl
{
    public bool ShowIcon => Icon != null;
    public double? SubtitleHeight => SubtitleVisible ? null : 0;
    public double? AdminRequiredHeight => AdminRequired ? null : 0;
    public double? RestartRequiredHeight => RestartRequired ? null : 0;
    
    public static readonly StyledProperty<StreamGeometry?> IconProperty = 
        AvaloniaProperty.Register<Setting, StreamGeometry?>(nameof(Icon));

    public static readonly StyledProperty<string> TitleProperty = 
        AvaloniaProperty.Register<Setting, string>(nameof(Title));

    public static readonly StyledProperty<string> SubtitleProperty = 
        AvaloniaProperty.Register<Setting, string>(nameof(Subtitle));
    
    public static readonly StyledProperty<bool> AdminRequiredProperty = 
        AvaloniaProperty.Register<Setting, bool>(nameof(AdminRequired));
    
    public static readonly StyledProperty<bool> RestartRequiredProperty = 
        AvaloniaProperty.Register<Setting, bool>(nameof(RestartRequired));

    public StreamGeometry? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
    
    public bool AdminRequired
    {
        get => GetValue(AdminRequiredProperty);
        set => SetValue(AdminRequiredProperty, value);
    }
    
    public bool RestartRequired
    {
        get => GetValue(RestartRequiredProperty);
        set => SetValue(RestartRequiredProperty, value);
    }

    public int TitleRowSpan => 1 + (SubtitleVisible ? 0 : 1) + (AdminRequired ? 0 : 1) + (RestartRequired ? 0 : 1);

    public bool SubtitleVisible => !string.IsNullOrEmpty(Subtitle);

    public Setting()
    {
        InitializeComponent();
    }
}