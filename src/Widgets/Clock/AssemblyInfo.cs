using System.Reflection;
using Clock.Locales;
using Clock.Models;
using Clock.Views;
using Clock.Views.Settings;
using uWidgets.Core.Models;
using uWidgets.Core.Models.Attributes;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.3")]

[assembly: WidgetInfo(typeof(AnalogI), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogI_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogIII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogIII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(Digital), typeof(ClockModel), typeof(DigitalClockSettings), "Clock_Digital_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(World), typeof(WorldClockModel), typeof(WorldClockSettings), "Clock_World_Title", "Clock_World_Subtitle")]
[assembly: Locale(typeof(Locale))]