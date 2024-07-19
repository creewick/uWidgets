using System.Reflection;
using Notes.Locales;
using Notes.Models;
using Notes.Views;
using uWidgets.Core.Models;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.0")]

[assembly: WidgetInfo(typeof(Note), typeof(NoteModel), null, "Notes_Title", "Notes_Subtitle")] 
[assembly: Locale(typeof(Locale))]