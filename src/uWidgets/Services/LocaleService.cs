using System.Globalization;
using System.Threading;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.Services;

public class LocaleService : ILocaleService
{
    public LocaleService(IAppSettingsProvider appSettingsProvider)
    {
        appSettingsProvider.DataChanging += (_, _, newSettings) => 
            SetCulture(newSettings.Region.Language);
    }
    
    public void SetCulture(string cultureName)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        Locale.Culture = Thread.CurrentThread.CurrentUICulture;
    }
}