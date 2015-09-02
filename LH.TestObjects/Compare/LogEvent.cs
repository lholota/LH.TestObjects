namespace LH.TestObjects.Compare
{
    /// <summary>
    /// Event raised during a comparison to be logged.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// The event level which is used to filter the events by minimum level configuration.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// The event description.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The summary of the comparison during which the event was raised.
        /// </summary>
        public IComparisonContext Context { get; set; }
    }
}