using System;
using System.Threading.Tasks;
using RxPipe.Lib.Models;
using RxPipe.Lib.Utilities;

namespace RxPipe.Lib.Processors
{
    /// <summary>
    /// Appends unique identificators for <see cref="Book"/>s.
    /// </summary>
    public class BookIdAppender : IPipeProcessor<Book>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of <see cref="BookIdAppender"/>.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> for logging process status.</param>
        public BookIdAppender(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Book> ProcessAsync(Book item)
        {
            // Simulate long running task.
            await Task.Delay(1000);
            item.Id = Guid.NewGuid();
            _logger.Write($"Appended id {item.Id} to {item.Title}.");
            return item;
        }
    }
}
