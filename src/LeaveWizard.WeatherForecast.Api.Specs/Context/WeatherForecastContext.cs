using System;
using System.Collections.Generic;

namespace LeaveWizard.WeatherForecast.Api.Specs.Context
{
    public class WeatherForecastContext
    {
        public List<WeatherForecast> PredictedForecast { get; set; }
        public List<WeatherForecast> ReceivedForecast { get; set; }

        public void PredictAStorm()
        {
            PredictedForecast = new List<WeatherForecast>
            {
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(1),
                    TemperatureC = 15,
                    Summary = "Stormy"
                }
            };
        }
    }
}