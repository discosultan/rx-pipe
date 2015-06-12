using System.Threading.Tasks;
using RxPipe.Lib.Models;
using RxPipe.Lib.Utilities;

namespace RxPipe.Lib.Processors
{
    /// <summary>
    /// Capitalizes titles for <see cref="Book"/>s.
    /// </summary>
    public class BookTitleCapitalizer : IPipeProcessor<Book>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of <see cref="BookTitleCapitalizer"/>.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> for logging process status.</param>
        public BookTitleCapitalizer(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<Book> ProcessAsync(Book item)
        {
            // Simulate long running task.
            await Task.Delay(1000);
            item.Title = item.Title.ToUpper();
            _logger.Write($"Capitalized title for {item.Title}.");
            return item;
        }
    }
}
