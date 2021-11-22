using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public class SpecFlowWebApplicationFactory : WebApplicationFactory<SpecFlowStartup>
    {
        private readonly IComponentContext _componentContext;

        public SpecFlowWebApplicationFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(x => new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup(ctx => new SpecFlowStartup(ctx.Configuration, _componentContext)).UseTestServer();
                });
            return builder;
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DebugLoggerProvider>());
            });
        }
        
        #region Debug Logger
        
        #endregion
    }
}