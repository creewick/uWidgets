using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace Monitor.Views.Controls;

public partial class Metric : Viewbox
{
    public Metric()
    {
        InitializeComponent();
        ProgressBar.Transitions = new Transitions
        {
            new DoubleTransition { Property = Shape.StrokeDashOffsetProperty, Duration = TimeSpan.FromMilliseconds(300) }
        };
    }
}