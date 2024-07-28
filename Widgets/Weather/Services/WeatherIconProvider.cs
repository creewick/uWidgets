using Avalonia.Media;
using Weather.Models.Forecast;
using Weather.ViewModels;
using Exception = System.Exception;

namespace Weather.Services;

public static class WeatherIconProvider
{
    public static StreamGeometry GetIcon(WeatherCode code)
    {
        return code switch
        {
            WeatherCode.ClearSky 
                or WeatherCode.MostlyClear 
                => WeatherIcon.Clear,
            WeatherCode.PartlyCloudy
                => WeatherIcon.PartlyClear,
            WeatherCode.Overcast 
                => WeatherIcon.Cloudy,
            WeatherCode.Fog 
                or WeatherCode.DepositingRimeFog 
                => WeatherIcon.Fog,
            WeatherCode.DrizzleLightIntensity 
                or WeatherCode.DrizzleModerateIntensity 
                or WeatherCode.DrizzleDenseIntensity
                or WeatherCode.FreezingDrizzleLightIntensity
                or WeatherCode.FreezingDrizzleDenseIntensity
                => WeatherIcon.Drizzle,
            WeatherCode.RainSlightIntensity
                or WeatherCode.RainModerateIntensity
                or WeatherCode.FreezingRainLightIntensity
                => WeatherIcon.Rain,
            WeatherCode.RainHeavyIntensity
                or WeatherCode.RainShowersSlightIntensity
                or WeatherCode.RainShowersModerateIntensity
                or WeatherCode.RainShowersViolentIntensity
                or WeatherCode.FreezingRainHeavyIntensity
                => WeatherIcon.HeavyRain,
            WeatherCode.SnowFallSlightIntensity
                or WeatherCode.SnowFallModerateIntensity
                => WeatherIcon.Snow,
            WeatherCode.SnowFallHeavyIntensity
                or WeatherCode.SnowGrains
                or WeatherCode.SnowShowersSlightIntensity
                or WeatherCode.SnowShowersHeavyIntensity
                => WeatherIcon.HeavySnow,
            WeatherCode.ThunderstormSlightIntensity
                or WeatherCode.ThunderstormWithSlightHail
                or WeatherCode.ThunderstormWithHeavyHail
                => WeatherIcon.Thunderstorm,
            _ => new StreamGeometry()
        };
    }
}