using System;
using Microsoft.Extensions.Logging;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    [ProviderAlias("Debug")]
    // ReSharper disable once ClassNeverInstantiated.Local
    public class DebugLoggerProvider : ILoggerProvider, ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string name) => new DebugLogger(name);

        public void Dispose()
        {
        }
    }
}