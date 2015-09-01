namespace LH.TestObjects.Compare
{
    using System;

    internal class Logger : ILoggerConfiguration
    {
        private bool isConsoleEnabled;
        private LogLevel minimumLevel;
        private Action<IComparisonContext> customLoggerCallback;

        public ILoggerConfiguration ToConsoleOutput(bool enabled = true)
        {
            this.isConsoleEnabled = enabled;
            return this;
        }

        public ILoggerConfiguration Callback(Action<IComparisonContext> callback)
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

            if (this.isConsoleEnabled)
            {
                throw new NotImplementedException();
            }

            if (this.customLoggerCallback != null)
            {
                this.customLoggerCallback.Invoke(context); // TODO: Create a new object and the context with the message!
            }
        }
    }
}