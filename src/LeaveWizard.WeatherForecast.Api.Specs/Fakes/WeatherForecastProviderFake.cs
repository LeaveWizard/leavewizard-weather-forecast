using System;
using System.Collections.Generic;
using LeaveWizard.WeatherForecast.Api.Forecasting;
using LeaveWizard.WeatherForecast.Api.Specs.Context;

namespace LeaveWizard.WeatherForecast.Api.Specs.Fakes
{
    public class WeatherForecastProviderFake : IWeatherForecastProvider
    {
        private readonly WeatherForecastContext _weatherForecastContext;

        public WeatherForecastProviderFake(WeatherForecastContext weatherForecastContext)
        {
            _weatherForecastContext = weatherForecastContext;
        }

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return _weatherForecastContext.PredictedForecast;
        }
    }
}