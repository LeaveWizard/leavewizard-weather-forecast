using System.Linq;
using System.Text.Encodings.Web;
using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using Moq;
using SpecFlow.Autofac;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    public static class AutofacContainerCreator {
        
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            
            var authOptionsFactory = new Mock<IOptionsFactory<TestAuthenticationOptions>>();
            authOptionsFactory.Setup(x => x.Create(It.IsAny<string>()))
                .Returns<string>(name => new TestAuthenticationOptions());
            
            containerBuilder.Register(_ => new OptionsMonitor<TestAuthenticationOptions>(authOptionsFactory.Object, 
                Enumerable.Empty<IOptionsChangeTokenSource<TestAuthenticationOptions>>(), 
                 new OptionsCache<TestAuthenticationOptions>())).As<IOptionsMonitor<TestAuthenticationOptions>>();
            containerBuilder.Register(_ => new DebugLoggerProvider()).SingleInstance().As<ILoggerFactory>();
            containerBuilder.Register(_ => new UrlTestEncoder()).As<UrlEncoder>();
            containerBuilder.Register(_ => new SystemClock()).As<ISystemClock>();
            //We're registering all test classes from assembly with [Binding] annotation
            containerBuilder.RegisterTypes(typeof(AutofacContainerCreator).Assembly.GetTypes()
                    //.Where(t => Attribute.IsDefined(t, typeof(BindingAttribute)))
                    .ToArray())
                .SingleInstance();

            return containerBuilder;
        }
    }
}