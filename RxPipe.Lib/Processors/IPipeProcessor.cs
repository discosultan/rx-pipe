using System.Threading.Tasks;

namespace RxPipe.Lib.Processors
{
    public interface IPipeProcessor<T>
    {
        Task<T> ProcessAsync(T item);
    }
}