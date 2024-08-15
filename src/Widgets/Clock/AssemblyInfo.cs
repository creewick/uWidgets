<<<<<<< HEAD
﻿using System.Reflection;
using Clock.Locales;
using Clock.Models;
using Clock.Views;
using Clock.Views.Settings;
using uWidgets.Core.Models.Attributes;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.4")]

[assembly: WidgetInfo(typeof(AnalogI), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogI_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogIII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogIII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(Digital), typeof(ClockModel), typeof(DigitalClockSettings), "Clock_Digital_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(World), typeof(WorldClockModel), typeof(WorldClockSettings), "Clock_World_Title", "Clock_World_Subtitle")]
=======
﻿using System.Reflection;
using Clock.Locales;
using Clock.Models;
using Clock.Views;
using Clock.Views.Settings;
using uWidgets.Core.Models.Attributes;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.4")]

[assembly: WidgetInfo(typeof(AnalogI), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogI_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(AnalogIII), typeof(ClockModel), typeof(AnalogClockSettings), "Clock_AnalogIII_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(Digital), typeof(ClockModel), typeof(DigitalClockSettings), "Clock_Digital_Title", "Clock_Analog_Subtitle")]
[assembly: WidgetInfo(typeof(World), typeof(WorldClockModel), typeof(WorldClockSettings), "Clock_World_Title", "Clock_World_Subtitle")]
>>>>>>> parent of 15524c5 (Delete src directory)
[assembly: Locale(typeof(Locale), "Clock", "M24,5 C34.4934102,5 43,13.5065898 43,24 C43,34.4934102 34.4934102,43 24,43 C13.5065898,43 5,34.4934102 5,24 C5,13.5065898 13.5065898,5 24,5 Z M24,7.5 C14.8873016,7.5 7.5,14.8873016 7.5,24 C7.5,33.1126984 14.8873016,40.5 24,40.5 C33.1126984,40.5 40.5,33.1126984 40.5,24 C40.5,14.8873016 33.1126984,7.5 24,7.5 Z M22.75,12 C23.3972087,12 23.9295339,12.4918747 23.9935464,13.1221948 L24,13.25 L24,24 L30.75,24 C31.4403559,24 32,24.5596441 32,25.25 C32,25.8972087 31.5081253,26.4295339 30.8778052,26.4935464 L30.75,26.5 L22.75,26.5 C22.1027913,26.5 21.5704661,26.0081253 21.5064536,25.3778052 L21.5,25.25 L21.5,13.25 C21.5,12.5596441 22.0596441,12 22.75,12 Z")]