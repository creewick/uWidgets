using uWidgets.Core.Models;

namespace uWidgets.Core.Interfaces;

public interface IWidgetLayoutProvider : IDataProvider<WidgetLayout>
{
    public void Remove();
}
