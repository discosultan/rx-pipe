using System.Threading.Tasks;

namespace RxPipe.Lib.Providers
{
    /// <summary>
    /// Contract for <see cref="ReactivePipe{T}"/> provider. The sole reason of the
    /// provider is to fetch items into the pipe.
    /// </summary>
    /// <typeparam name="T">Type of items to fetch.</typeparam>
    public interface IPipeProvider<T>
    {
        bool HasNext { get; }

        /// <summary>
        /// Item provision.
        /// </summary>
        /// <returns>Cold observable.</returns>
        Task<T> GetNextAsync();
    }
}