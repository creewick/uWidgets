using Avalonia.Controls;

namespace uWidgets.Settings;

public partial class MainWindow : Window
{
    public ListItemTemplate[] MenuItems =>
    [
        new ListItemTemplate(typeof(MainWindow), "Appearance"),
        new ListItemTemplate(typeof(MainWindow), "Language"),
        new ListItemTemplate(typeof(MainWindow), "About")
    ];

    public MainWindow()
    {
        DataContext = this;
        Resized += OnResized;
        InitializeComponent();
    }

    private void OnResized(object? sender, WindowResizedEventArgs e)
    {
        SplitView.IsPaneOpen = Width >= 800;
    }
}