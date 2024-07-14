using System.Reflection;
using Calendar.Views;
using uWidgets.Core.Models;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.0")]

[assembly: WidgetInfo(typeof(Month), null, null, "Calendar_Month_Title", "Calendar_Month_Subtitle")]
[assembly: WidgetInfo(typeof(Date), null, null, "Calendar_Date_Title", "Calendar_Date_Subtitle")]

// [assembly: WidgetInfo(typeof(AnalogI), typeof(ClockModel), "Clock_AnalogI_Title", "Clock_Analog_Subtitle")]
// [assembly: WidgetInfo(typeof(AnalogII), typeof(ClockModel), "Clock_AnalogII_Title", "Clock_Analog_Subtitle")]
// [assembly: WidgetInfo(typeof(AnalogIII), typeof(ClockModel), "Clock_AnalogIII_Title", "Clock_Analog_Subtitle")]
// [assembly: WidgetInfo(typeof(Digital), typeof(ClockModel), "Clock_Digital_Title", "Clock_Analog_Subtitle")]
// [assembly: WidgetInfo(typeof(World), typeof(WorldClockModel), "Clock_World_Title", "Clock_World_Subtitle")]
// [assembly: Locale(typeof(Locale))]