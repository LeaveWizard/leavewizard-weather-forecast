using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Context
{
    public class UserContext
    {
        public string Email { get; set; }
        public string AuthToken { get; set; }
    }
}