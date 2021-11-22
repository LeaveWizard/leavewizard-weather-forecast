using System.Collections.Generic;

namespace LeaveWizard.WeatherForecast.Api.Forecasting
{
    public interface IWeatherForecastProvider
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}