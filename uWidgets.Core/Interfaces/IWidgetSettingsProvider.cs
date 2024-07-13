using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IWidgetSettingsProvider : IDataProvider<WidgetSettings>
{
    public void Remove();
}
