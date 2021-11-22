using System;
using System.Net.Http;
using Autofac;
using FluentAssertions;
using LeaveWizard.WeatherForecast.Api.Specs.Core;

namespace LeaveWizard.WeatherForecast.Api.Specs.Context
{
    public class AppHostingContext : IDisposable
    {
        private readonly IComponentContext _componentContext;
        private static SpecFlowWebApplicationFactory _webApplicationFactory;

        public AppHostingContext(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        
        public HttpClient CreateClient()
        {
            StartApp();

            _webApplicationFactory.Should().NotBeNull("the app should be running");
            return _webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            //nop
        }

        public void StartApp()
        {
            if (_webApplicationFactory == null)
            {
                Console.WriteLine("Starting Web Application...");
                _webApplicationFactory = new SpecFlowWebApplicationFactory(_componentContext);
            }
            else
            {
                _webApplicationFactory = new SpecFlowWebApplicationFactory(_componentContext);
            }
        }

        public static void StopApp()
        {
            if (_webApplicationFactory != null)
            {
                Console.WriteLine("Shutting down Web Application...");
                _webApplicationFactory.Dispose();
                _webApplicationFactory = null;
            }
        }
    }
}