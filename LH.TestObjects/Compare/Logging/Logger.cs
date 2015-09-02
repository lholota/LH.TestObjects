namespace LH.TestObjects.Compare.Logging
{
    using System;

    internal class Logger : ILoggerConfiguration
    {
        private LogLevel minimumLevel;
        private Action<LogEvent> customLoggerCallback;

        public ILoggerConfiguration Callback(Action<LogEvent> callback)
        {
            this.customLoggerCallback = callback;
            return this;
        }

        public ILoggerConfiguration SetLevel(LogLevel level)
        {
            this.minimumLevel = level;
            return this;
        }

        public void Log(LogLevel level, IComparisonContext context, string message, params object[] args)
        {
            if (level < this.minimumLevel)
            {
                return;
            }
            
            if (this.customLoggerCallback != null)
            {
                var logEvent = new LogEvent
                {
                    Level = level,
                    Message = string.Format(message, args),
                    Context = context
                };

                this.customLoggerCallback.Invoke(logEvent);
            }
        }
    }
}