using System.Reflection;
using Calendar.Locales;
using Calendar.Models;
using Calendar.Views;
using Calendar.Views.Settings;
using uWidgets.Core.Models;
using uWidgets.Core.Models.Attributes;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.3")]

[assembly: WidgetInfo(typeof(Date), null, null, "Calendar_Date_Title", "Calendar_Date_Subtitle")]
[assembly: WidgetInfo(typeof(Month), typeof(MonthCalendarModel), typeof(MonthCalendarSettings), "Calendar_Month_Title", "Calendar_Month_Subtitle")]
[assembly: Locale(typeof(Locale))]