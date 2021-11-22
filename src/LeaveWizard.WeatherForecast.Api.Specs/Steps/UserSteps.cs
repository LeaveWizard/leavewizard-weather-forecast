using System.Linq;
using System.Net;
using FluentAssertions;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly UserContext _userContext;
        private readonly WebApiContext _webApiContext;
    
        public UserSteps(UserContext userContext, 
            WebApiContext webApiContext) 
        {
            _userContext = userContext;
            _webApiContext = webApiContext;
        }

        [Then(@"an unauthorised error is returned")]
        public void ThenAnUnauthorisedErrorIsReturned()
        {
            var response = _webApiContext.ApiResponses.Last();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

    }
}