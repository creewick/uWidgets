<<<<<<< HEAD
using Avalonia.Controls;
using Monitor.Models;
using Monitor.ViewModels;

namespace Monitor.Views;

public partial class SingleMetric : UserControl
{
    public SingleMetric() :
        this(new SingleMetricModel(MetricType.CpuUsage)) {}
    
    public SingleMetric(SingleMetricModel model)
    {
        DataContext = new SingleMetricViewModel(model);
        InitializeComponent();
    }
=======
using Avalonia.Controls;
using Monitor.Models;
using Monitor.ViewModels;

namespace Monitor.Views;

public partial class SingleMetric : UserControl
{
    public SingleMetric() :
        this(new SingleMetricModel(MetricType.CpuUsage)) {}
    
    public SingleMetric(SingleMetricModel model)
    {
        DataContext = new SingleMetricViewModel(model);
        InitializeComponent();
    }
>>>>>>> parent of 15524c5 (Delete src directory)
}