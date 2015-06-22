using System.Collections.Generic;
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
        private readonly Queue<Book> _books = new Queue<Book>(new []
        {
            new Book {Title = "Tom Sawyer"},
            new Book {Title = "Harry Potter"},
            new Book {Title = "Hunger Games"},
            new Book {Title = "The Song of Ice and Fire"}
        });

        /// <summary>
        /// Constructs a new instance of <see cref="FakeBookProvider"/>.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> for logging process status.</param>
        public FakeBookProvider(ILogger logger)
        {
            _logger = logger;
        }

        public bool HasNext => _books.Count > 0;

        public async Task<Book> GetNextAsync()
        {
            // Simulate long running task.
            await Task.Delay(1000);
            var book = _books.Dequeue();
            _logger.Write($"Provided {book.Title}.");
            return book;
        }
    }
}
