using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using LeaveWizard.WeatherForecast.Api.Specs.Models;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Steps
{
    [Binding]
    public class WeatherPredictionSteps
    { 
        private readonly UserContext _userContext;
        private readonly WebApiContext _webApiContext;
        private readonly WeatherForecastContext _weatherForecastContext;

        public WeatherPredictionSteps(UserContext userContext, 
            WebApiContext webApiContext,
            WeatherForecastContext weatherForecastContext) 
        {
            _userContext = userContext;
            _webApiContext = webApiContext;
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
            var response = _webApiContext.ExecuteGet<List<WeatherForecast>>(EndpointRoutes.GetWeatherForecast);
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