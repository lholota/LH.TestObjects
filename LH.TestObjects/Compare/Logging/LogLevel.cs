namespace LH.TestObjects.Compare.Logging
{
    /// <summary>
    /// The logging level classification for the events.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug messages are used to provide verbose info and are not related to the values which are being compared.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Info messages are logged to show what is happening inside of the comparator/generator.
        /// </summary>
        Info = 1,

        /// <summary>
        /// The error messages sign that a difference has been found in the comparator.
        /// </summary>
        Error = 2
    }
}