<<<<<<< HEAD
using Avalonia.Controls;
using Avalonia.Media;

namespace Weather.ViewModels;

=======
using Avalonia.Controls;
using Avalonia.Media;

namespace Weather.ViewModels;

>>>>>>> parent of 15524c5 (Delete src directory)
public record DailyForecastViewModel(string DayOfWeek, StreamGeometry Icon, string Min, string Max, List<GridLength> Graph);