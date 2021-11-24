using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using LeaveWizard.WeatherForecast.Api.Specs.Drivers;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Steps
{
    [Binding]
    public class WeatherPredictionSteps
    { 
        private readonly WebApiDriver _webApiDriver;
        private readonly WeatherForecastContext _weatherForecastContext;

        public WeatherPredictionSteps( 
            WebApiDriver webApiDriver,
            WeatherForecastContext weatherForecastContext) 
        {
            _webApiDriver = webApiDriver;
            _weatherForecastContext = weatherForecastContext;
        }
        
        [Given(@"there is a storm coming")]
        public void GivenThereIsAStormComing()
        {
            _weatherForecastContext.PredictAStorm();
        }

        [When(@"a request is made to predict the weather")]
        public void WhenARequestIsMadeToPredictTheWeather()
        {
            var response = _webApiDriver.ExecuteGet<List<WeatherForecast>>(EndpointRoutes.GetWeatherForecast);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            _weatherForecastContext.ReceivedForecast = response.ResponseData; 
        }

        [Then(@"a storm is correctly predicted")]
        public void ThenAStormIsCorrectlyPredicted()
        {
            _weatherForecastContext
                .ReceivedForecast
                .Should()
                .BeEquivalentTo(_weatherForecastContext.PredictedForecast);
        }
    }
}