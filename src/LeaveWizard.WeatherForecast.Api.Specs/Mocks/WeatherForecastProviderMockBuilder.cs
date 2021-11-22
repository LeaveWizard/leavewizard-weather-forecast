using LeaveWizard.WeatherForecast.Api.Forecasting;
using Moq;
using LeaveWizard.WeatherForecast.Api.Specs.Fakes;

namespace LeaveWizard.WeatherForecast.Api.Specs.Mocks
{
    public class WeatherForecastProviderMockBuilder
    {
        private readonly WeatherForecastProviderFake _weatherForecastProviderFake;
     
        public WeatherForecastProviderMockBuilder(WeatherForecastProviderFake weatherForecastProviderFake)
        {
            _weatherForecastProviderFake = weatherForecastProviderFake;
        }

        public Mock<IWeatherForecastProvider> Build()
        {
            var mockDataStore = new Mock<IWeatherForecastProvider>(MockBehavior.Strict);
            
            mockDataStore.Setup(x => x.GetWeatherForecast()).Returns(() => _weatherForecastProviderFake.GetWeatherForecast());
            
            return mockDataStore; 
        }
    }
}