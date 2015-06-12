namespace RxPipe.Lib.Utilities
{
    /// <summary>
    /// Contract for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Outputs a log entry.
        /// </summary>
        /// <param name="entry">Entry to write.</param>
        void Write(string entry);
    }
}
