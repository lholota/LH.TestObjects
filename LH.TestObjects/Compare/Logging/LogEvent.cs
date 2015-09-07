namespace LH.TestObjects.Compare.Logging
{
    /// <summary>
    /// Event raised during a comparison to be logged.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// Gets the event level which is used to filter the events by minimum level configuration.
        /// </summary>
        public LogLevel Level { get; internal set; }

        /// <summary>
        /// Gets the event description.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Gets the summary of the comparison during which the event was raised.
        /// </summary>
        public IValueComparison Comparison { get; internal set; }
    }
}