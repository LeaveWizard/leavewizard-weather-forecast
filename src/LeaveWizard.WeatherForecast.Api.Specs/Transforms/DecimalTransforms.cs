using TechTalk.SpecFlow;

namespace LeaveWizard.WeatherForecast.Api.Specs.Transforms
{
    [Binding]
    public class DecimalTransforms
    {
        [StepArgumentTransformation()]
        public decimal? TransformInOrNot(string decimalStringValue)
        {
            if(!decimal.TryParse(decimalStringValue, out decimal result))
            {
                return default(decimal?);
            }

            return result;
        }
    }
}