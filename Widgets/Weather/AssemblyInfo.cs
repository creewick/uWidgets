using System.Reflection;
using uWidgets.Core.Models;
using Weather.Models;
using Weather.Views;
using Weather.Views.Settings;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.1")]

[assembly: WidgetInfo(typeof(Forecast), typeof(ForecastModel), typeof(ForecastSettings), "Weather_Forecast_Title", "Weather_Forecast_Subtitle")]