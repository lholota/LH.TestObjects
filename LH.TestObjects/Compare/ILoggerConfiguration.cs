namespace LH.TestObjects.Compare
{
    using System;

    /// <summary>
    /// Allows the fluent configuration of logging.
    /// </summary>
    public interface ILoggerConfiguration
    {
        /// <summary>
        /// The comparator will log the messages into the standard console output.
        /// </summary>
        void ToConsoleOutput(); // TODO: Add parameter whether to only include differences or all messages

        /// <summary>
        /// The log messages will be passed to the callback where you can apply any logging logic you require.
        /// </summary>
        /// <param name="callback">Your custom logging action.</param>
        void Callback(Action<IComparisonContext> callback);
    }
}