namespace Reminders.Models;

public record RemindersListModel(string? ListName, List<ReminderModel> Reminders);