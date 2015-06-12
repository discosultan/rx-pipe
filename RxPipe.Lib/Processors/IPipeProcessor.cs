using System.Threading.Tasks;

namespace RxPipe.Lib.Processors
{
    /// <summary>
    /// Contract for <see cref="ReactivePipe{T}"/> processor.    
    /// </summary>
    /// <typeparam name="T">Type of item to process.</typeparam>
    /// <remarks>    
    /// input > [ processor 1 > processor 2 > ... > processor n ] > output
    /// </remarks>
    public interface IPipeProcessor<T>
    {
        /// <summary>
        /// Processes the item in an async fashion.
        /// </summary>
        /// <param name="item">Type of item to process.</param>
        /// <returns>Processed item.</returns>
        Task<T> ProcessAsync(T item);        
    }
}