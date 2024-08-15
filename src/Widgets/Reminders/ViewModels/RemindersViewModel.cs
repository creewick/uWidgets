<<<<<<< HEAD
using ReactiveUI;
using Reminders.Models;

namespace Reminders.ViewModels;

public class RemindersViewModel(RemindersListModel model) : ReactiveObject
{
    public string? ListName => model.ListName;
    public IEnumerable<ReminderModel> Reminders => model.Reminders;
    public int Count => model.Reminders.Count;
=======
using ReactiveUI;
using Reminders.Models;

namespace Reminders.ViewModels;

public class RemindersViewModel(RemindersListModel model) : ReactiveObject
{
    public string? ListName => model.ListName;
    public IEnumerable<ReminderModel> Reminders => model.Reminders;
    public int Count => model.Reminders.Count;
>>>>>>> parent of 15524c5 (Delete src directory)
}