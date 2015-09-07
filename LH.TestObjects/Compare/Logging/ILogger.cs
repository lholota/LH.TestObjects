namespace LH.TestObjects.Compare.Logging
{
    internal interface ILogger : ILoggerConfiguration
    {
        /// <summary>
        /// Log a message with the Info level.
        /// </summary>
        /// <param name="level">Level of the logged event</param>
        /// <param name="context">Context of the currently performed comparison</param>
        /// <param name="message">Message or message format</param>
        /// <param name="args">Parameters to be applied in the string.Format</param>
        void Log(LogLevel level, IValueComparison context, string message, params object[] args);
    }
}