using System.Reflection;
using Clock.Models;
using Clock.Views;
using uWidgets.Core.Models;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.1")]

[assembly: WidgetInfo(typeof(AnalogI), typeof(ClockModel))]
[assembly: WidgetInfo(typeof(AnalogII), typeof(ClockModel))]
[assembly: WidgetInfo(typeof(AnalogIII), typeof(ClockModel))]
[assembly: WidgetInfo(typeof(Digital), typeof(ClockModel))]
[assembly: WidgetInfo(typeof(World), typeof(WorldClockModel))]
