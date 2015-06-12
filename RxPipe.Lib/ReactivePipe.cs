using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RxPipe.Lib.Processors;
using RxPipe.Lib.Providers;
using RxPipe.Lib.Utilities;

namespace RxPipe.Lib
{
    /// <summary>
    /// Provides push-based processing pipeline.
    /// </summary>
    /// <typeparam name="T">Type of items coming through the pipe.</typeparam>
    /// <remarks>    
    /// input > [ processor 1 > processor 2 > ... > processor n ] > output
    /// </remarks>
    public class ReactivePipe<T>
    {
        private readonly IPipeProvider<T> _provider;
        private readonly IPipeProcessor<T>[] _processors;

        /// <summary>
        /// Constructs a new instance of <see cref="ReactivePipe{T}"/>.
        /// </summary>
        /// <param name="provider">Provider for providing items for the pipe.</param>
        /// <param name="processors">Processors for processing items flowing through the pipe.</param>
        public ReactivePipe(IPipeProvider<T> provider, params IPipeProcessor<T>[] processors)
        {
            Guard.NotNull(provider, nameof(provider));
            Guard.NotNull(processors, nameof(processors));
            _provider = provider;
            _processors = processors;
        }

        /// <summary>
        /// Processed item provision.
        /// </summary>
        /// <returns>Cold observable.</returns>
        public IObservable<T> WhenProcessed()
        {
            return _provider.WhenProvided().Select(x =>
            {
                return Observable.FromAsync(() =>
                {                    
                    return _processors.Aggregate(
                        seed: Task.FromResult(x), 
                        func: (current, processor) => current.ContinueWith( // Append continuations.
                            previousTask => processor.ProcessAsync(previousTask.Result))
                            .Unwrap()); // We need to unwrap Task{T} from Task{Task{T}}.
                });
            }).Concat();
        }
    }
}
