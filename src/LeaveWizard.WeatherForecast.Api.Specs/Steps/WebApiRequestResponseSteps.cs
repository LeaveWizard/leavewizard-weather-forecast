using System.Linq;
using FluentAssertions;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Steps
{
    [Binding]
    public class WebApiRequestResponseSteps
    {
        private readonly WebApiContext _webApiContext;
         
        public WebApiRequestResponseSteps(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }
        
        [Then(@"the returned request is not a success")]
        public void ThenTheReturnedRequestIsNotASuccess()
        {
            _webApiContext.ApiResponses.Last().IsSuccessStatusCode.Should().BeFalse();
        }
         
        [Then(@"the returned request is a success")]
        public void ThenTheReturnedRequestIsASuccess()
        {
            _webApiContext.ApiResponses.Last().IsSuccessStatusCode.Should().BeTrue();
        }
    }
}