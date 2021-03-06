﻿namespace LH.TestObjects.Compare.Logging
{
    using System;

    internal class Logger : ILogger
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

        public void Log(LogLevel level, IValueComparison comparison)
        {
            var message = string.Format("{0}: {1}", comparison.PropertyPath, comparison.Message);
            this.Log(level, comparison, message);
        }

        public void Log(LogLevel level, IValueComparison comparison, string message, params object[] args)
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
                    Comparison = comparison
                };

                this.customLoggerCallback.Invoke(logEvent);
            }
        }
    }
}