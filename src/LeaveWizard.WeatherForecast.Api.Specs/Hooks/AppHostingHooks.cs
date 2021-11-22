using LeaveWizard.WeatherForecast.Api.Specs.Context;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Hooks
{
    
    public class AppHostingHooks
    {
        [AfterTestRun]
        public static void StopApp()
        {
            AppHostingContext.StopApp();
        }
    }
}