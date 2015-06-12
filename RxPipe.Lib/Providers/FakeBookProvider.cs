using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RxPipe.Lib.Models;
using RxPipe.Lib.Utilities;

namespace RxPipe.Lib.Providers
{
    /// <summary>
    /// Fake <see cref="Book"/> provider for Contoso library.
    /// </summary>
    public class FakeBookProvider : IPipeProvider<Book>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of <see cref="FakeBookProvider"/>.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> for logging process status.</param>
        public FakeBookProvider(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public IObservable<Book> WhenProvided()
        {            
            return Observable.Create<Book>(
                async observable =>
                {
                    Queue<Book> books = QueueFakeBooks();
                    while (books.Count > 0)
                    {
                        // Simulate long running task.
                        await Task.Delay(1000);
                        Book book = books.Dequeue();
                        _logger.Write($"Provided {book.Title}.");
                        observable.OnNext(book);
                    }
                });
        }

        private static Queue<Book> QueueFakeBooks()
        {
            return new Queue<Book>(new []
            {
                new Book {Title = "Tom Sawyer"},
                new Book {Title = "Harry Potter"},
                new Book {Title = "Hunger Games"}
            });
        }
    }
}
