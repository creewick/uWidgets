using Avalonia.Controls;
using Clock.Models;
using Clock.ViewModels;

namespace Clock.Views;

public partial class World : UserControl
{
    public World() : this(new WorldClockModel([])) {}
    
    public World(WorldClockModel worldClockModel)
    {
        DataContext = new WorldClockViewModel(worldClockModel);
        Unloaded += (_, _) => ((WorldClockViewModel)DataContext).Dispose();
        InitializeComponent();
    }
}