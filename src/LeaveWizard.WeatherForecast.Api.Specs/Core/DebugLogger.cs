using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace LeaveWizard.WeatherForecast.Api.Specs.Core
{
    class DebugLogger : ILogger
    {
        private readonly string _name;

        public DebugLogger(string name)
        {
            _name = string.IsNullOrEmpty(name) ? nameof(DebugLogger) : name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Debugger.IsAttached && logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
                return;

            message = $"{ logLevel }: {message}";

            if (exception != null)
                message += Environment.NewLine + Environment.NewLine + exception;

            Debug.WriteLine(message, _name);
        }

        private class NoopDisposable : IDisposable
        {
            public static readonly NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
}