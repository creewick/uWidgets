using System.Reflection;
using Reminders.Locales;
using Reminders.Models;
using Reminders.Views;
using uWidgets.Core.Models;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.1")]

[assembly: WidgetInfo(typeof(List), typeof(RemindersListModel), null, "Reminders_List_Title", "Reminders_List_Subtitle")]
[assembly: Locale(typeof(Locale))]