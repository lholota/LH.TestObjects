﻿namespace LH.TestObjects.Compare.Logging
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

        /// <summary>
        /// Log a message with the Info level.
        /// </summary>
        /// <param name="level">Level of the logged event</param>
        /// <param name="context">Context of the currently performed comparison</param>
        /// <param name="message">Message or message format</param>
        /// <param name="args">Parameters to be applied in the string.Format</param>
        void Log(LogLevel level, IComparisonContext context, string message, params object[] args);
    }
}