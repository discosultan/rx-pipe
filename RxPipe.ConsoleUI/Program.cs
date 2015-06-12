using System;
using System.Reactive;
using RxPipe.Lib;
using RxPipe.Lib.Models;
using RxPipe.Lib.Processors;
using RxPipe.Lib.Providers;

namespace RxPipe.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipe = new ReactivePipe<Book>(
                new FakeBookProvider(),
                new BookTitleCapitalizer());            

            Console.WriteLine("Starting to process books.");            
            Console.WriteLine();

            pipe.Process().Subscribe(Observer.Create<Book>(
                onNext: article =>
                {
                    Console.WriteLine("Finished processing {0}.", article.Title);
                },
                onCompleted: () =>
                {
                    Console.WriteLine();
                    Console.WriteLine("Finished processing all the books.");
                }));
            
            Console.ReadKey();
        }        
    }
}
