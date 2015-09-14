namespace LH.TestObjects.Compare.Logging
{
    using System;

    /// <summary>
    /// Allows the fluent configuration of logging.
    /// </summary>
    public interface ILoggerConfiguration
    {
        /// <summary>
        /// The log messages will be passed to the callback where you can apply any logging logic you require.
        /// </summary>
        /// <param name="callback">Your custom logging action.</param>
        /// <returns><see cref="ILoggerConfiguration"/></returns>
        ILoggerConfiguration Callback(Action<LogEvent> callback);

        /// <summary>
        /// Sets the minimum level of the messages which will be logged.
        /// </summary>
        /// <param name="level">The minimum level of the logged events. Events will lower level will be ignored.</param>
        /// <returns><see cref="ILoggerConfiguration"/></returns>
        ILoggerConfiguration SetLevel(LogLevel level);
    }
}