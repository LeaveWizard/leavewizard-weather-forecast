using System; 
using System.Reflection;
using System.Text.Encodings.Web;
using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LeaveWizard.WeatherForecast.Api.Forecasting;
using LeaveWizard.WeatherForecast.Api.Specs.Context; 
using LeaveWizard.WeatherForecast.Api.Specs.Mocks;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public class SpecFlowStartup : Startup
    {
        private readonly IComponentContext _componentContext;

        public SpecFlowStartup(IConfiguration configuration, IComponentContext componentContext): base(configuration)
        {
            _componentContext = componentContext;
        }
        
        protected override IMvcBuilder ConfigureMvc(IServiceCollection services)
        {
            return base.ConfigureMvc(services)
                // This is necessary for the controller location logic to find the controllers when
                // using this startup since it is in a different assembly.    
                .AddApplicationPart(Assembly.GetAssembly(typeof(WeatherForecastController)));
        }

        protected override void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication("LeaveWizard.WeatherForecast.Application")
                    .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>("LeaveWizard.WeatherForecast", (Action<TestAuthenticationOptions>) null);
        }

        protected override void RegisterDependencies(ContainerBuilder builder)
         {
             base.RegisterDependencies(builder);

             DependencyRegistrar.RegisterDependencies(_componentContext, builder);
         }
    }
}