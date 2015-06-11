using System.Threading.Tasks;
using RxPipe.Lib.Models;

namespace RxPipe.Lib.Processors
{
    public class BookTitleCapitalizer : IPipeProcessor<Book>
    {
        public async Task<Book> ProcessAsync(Book item)
        {
            await Task.Delay(1000);
            item.Title = item.Title.ToUpper();
            return item;
        }
    }
}
