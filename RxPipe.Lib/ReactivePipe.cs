using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
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
        private readonly PipeSettings _settings;
        private readonly IPipeProvider<T> _provider;
        private readonly IPipeProcessor<T>[] _processors;

        /// <summary>
        /// Constructs a new instance of <see cref="ReactivePipe{T}"/>.
        /// </summary>
        /// <param name="settings">Pipe configuration arguments.</param>
        /// <param name="provider">Provider for providing items for the pipe.</param>
        /// <param name="processors">Processors for processing items flowing through the pipe.</param>
        public ReactivePipe(PipeSettings settings, IPipeProvider<T> provider, params IPipeProcessor<T>[] processors)
        {
            Guard.NotNull(provider, nameof(provider)); 
            Guard.NotNull(processors, nameof(processors));
            _settings = settings;
            _provider = provider;
            _processors = processors;
        }

        /// <summary>
        /// Processed item provision.
        /// </summary>
        /// <returns>Cold observable.</returns>
        public IObservable<T> WhenProcessed()
        {
            var semaphore = _settings.IsLimited
                ? new SemaphoreSlim(_settings.MaxNumParallelProcessings)
                : null;

            return Observable.Create<T>(observer =>
            {
                var cancel = new CancellationDisposable();
                IDisposable scheduledItem = Scheduler.Default.Schedule(async () =>
                {
                    while (_provider.HasNext && !cancel.Token.IsCancellationRequested)
                    {
                        await semaphore?.WaitAsync();
                        T item = await _provider.GetNextAsync();
                        observer.OnNext(item);
                    }
                    observer.OnCompleted();
                });
                return new CompositeDisposable(cancel, scheduledItem);
            }).SelectMany(item => Observable.FromAsync(() =>
                _processors.Aggregate(
                    seed: Task.FromResult(item),
                    func: (current, processor) => current.ContinueWith( // Append continuations.
                        previous => processor.ProcessAsync(previous.Result)).Unwrap()) // We need to unwrap Task{T} from Task{Task{T}};
            ).Finally(() =>
            {
                semaphore?.Release();
            }));
        }
    }
}
