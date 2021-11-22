using System.Text.Encodings.Web;
using Autofac;
using LeaveWizard.WeatherForecast.Api.Forecasting;
using LeaveWizard.WeatherForecast.Api.Specs.Context;
using LeaveWizard.WeatherForecast.Api.Specs.Mocks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(IComponentContext componentContext, ContainerBuilder builder)
        {
            // Add custom test dependency registrations here
            // The IComponentContext provides access to all the registrations created in the AutofacContainerCreator.CreateContainerBuilder method 
             
            builder.Register(_ => componentContext.Resolve<ISystemClock>()).As<ISystemClock>();
            builder.Register(_ => componentContext.Resolve<IOptionsMonitor<TestAuthenticationOptions>>()).As<IOptionsMonitor<TestAuthenticationOptions>>();
            builder.Register(_ => componentContext.Resolve<UrlEncoder>()).As<UrlEncoder>();
            builder.Register(_ => componentContext.Resolve<TestAuthenticationHandler>()).As<TestAuthenticationHandler>();
            builder.Register(_ => componentContext.Resolve<UserContext>()).SingleInstance().As<UserContext>();
            builder.Register(_ => componentContext.Resolve<ILoggerFactory>()).SingleInstance().As<ILoggerFactory>();
             
            builder.Register(x => componentContext.Resolve<WeatherForecastProviderMockBuilder>().Build().Object).SingleInstance().AsSelf().As<IWeatherForecastProvider>();
        }
    }
}