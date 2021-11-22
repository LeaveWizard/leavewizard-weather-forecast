using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeaveWizard.WeatherForecast.Api.Forecasting
{
    [ApiController]
    [Route(EndpointRoutes.ApiV1)]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastProvider _weatherForecastProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                         IWeatherForecastProvider weatherForecastProvider)
        {
            _logger = logger;
            _weatherForecastProvider = weatherForecastProvider;
        }

        [HttpGet(EndpointRoutes.GetWeatherForecast)]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastProvider.GetWeatherForecast();
        }
    }
}