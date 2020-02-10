namespace CRUDEF.Providers
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;

    public class crudefDbLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
                //switch (logLevel)
                //{
                //    case LogLevel.Information:
                //    //case LogLevel.Debug:
                //    case LogLevel.Error:
                //    case LogLevel.Warning:
                //        return true;
                //}

                //return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                string contents = formatter(state, exception);
                File.AppendAllText("log.txt", contents);
                Console.WriteLine(contents);
            }
        }
    }
}
