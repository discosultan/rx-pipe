using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RxPipe.Lib.Models;

namespace RxPipe.Lib.Providers
{
    public class FakeBookProvider : IPipeProvider<Book>
    {
        public IObservable<Book> GetAll()
        {            
            return Observable.Create<Book>(
                async observable =>
                {
                    Queue<Book> articles = QueueFakeArticles();
                    while (articles.Count > 0)
                    {
                        // Simulate long running task.
                        await Task.Delay(1000);
                        observable.OnNext(articles.Dequeue());
                    }
                });
        }

        private static Queue<Book> QueueFakeArticles()
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
