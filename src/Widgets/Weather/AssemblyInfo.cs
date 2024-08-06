using System.Reflection;
using uWidgets.Core.Models;
using uWidgets.Core.Models.Attributes;
using Weather.Models;
using Weather.Views;
using Weather.Views.Settings;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.1.0")]

[assembly: WidgetInfo(typeof(Forecast), typeof(ForecastModel), typeof(ForecastSettings), "Weather_Forecast_Title", "Weather_Forecast_Subtitle")]