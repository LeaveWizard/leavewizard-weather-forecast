using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Transforms
{
    [Binding]
    public class DateTransforms
    {
        [StepArgumentTransformation(@"(\d+):(\d+)")]
        public DateTime TransformTimeOfDay(int hour, int minute)
        {
            return new DateTime(1970, 1, 1, hour, minute, 0);
        }

        [StepArgumentTransformation(@"""(.*)""")]
        public DateTime TransformDate(string date)
        {
            return DateTime.Parse(date, CultureInfo.InvariantCulture);
        }
    }
}