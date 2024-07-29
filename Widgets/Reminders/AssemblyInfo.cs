using System.Reflection;
using Reminders.Locales;
using Reminders.Models;
using Reminders.Views;
using Reminders.Views.Settings;
using uWidgets.Core.Models;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.1.0")]

[assembly: WidgetInfo(typeof(List), typeof(RemindersListModel), typeof(ListSettings), "Reminders_List_Title", "Reminders_List_Subtitle")]
[assembly: Locale(typeof(Locale))]