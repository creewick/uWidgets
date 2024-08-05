using System.Text.Json;
using Calendar.Models;
using ReactiveUI;
using uWidgets.Core.Interfaces;

namespace Calendar.ViewModels;

public class MonthCalendarSettingsViewModel(IWidgetLayoutProvider widgetLayoutProvider) : ReactiveObject
{
    public MonthCalendarModel Model =>
        widgetLayoutProvider.Get().GetModel<MonthCalendarModel>() 
        ?? new MonthCalendarModel(DayOfWeek.Monday);

    public DayOfWeek[] Days => Enum.GetValues<DayOfWeek>();
    
    public DayOfWeek FirstDayOfWeek
    {
        get => Model.FirstDayOfWeek;
        set
        {
            var newModel = Model with { FirstDayOfWeek = value };
            var newSettings = widgetLayoutProvider.Get() with
            {
                Settings = JsonSerializer.SerializeToElement(newModel)
            };
            widgetLayoutProvider.Save(newSettings);
            this.RaisePropertyChanged();
        }
    }
}