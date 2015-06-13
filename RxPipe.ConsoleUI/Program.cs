using System;
using System.Reactive;
using RxPipe.Lib;
using RxPipe.Lib.Models;
using RxPipe.Lib.Processors;
using RxPipe.Lib.Providers;
using RxPipe.Lib.Utilities;

namespace RxPipe.ConsoleUI
{
    class Program
    {
        // We need a common object to lock on since some parts of the program
        // change the Console foreground color and writing to Console is done in
        // a multi-threaded environment (colors might get messed up otherwise).
        static readonly object _syncRoot = new object();

        static void Main(string[] args)
        {            
            ILogger logger = new ConsoleLogger(_syncRoot) { Color = ConsoleColor.Green };

            var pipe = new ReactivePipe<Book>(
                new FakeBookProvider(logger), // Provider.
                new BookTitleCapitalizer(logger), // 1st processor.
                new BookIdAppender(logger)); // 2nd processor.
            
            Console.WriteLine("Welcome to Contoso book processing pipeline. Press any key to start...\n");
            Console.ReadKey(true);

            Console.WriteLine("Starting to process books. Press any key to exit...\n");         

            pipe.WhenProcessed().Subscribe(
                onNext: book =>
                {
                    WriteSync($"\nFinished processing {book.Title}.\n");                    
                },
                onCompleted: () =>
                {
                    WriteSync("Finished processing all the books.");
                });
            
            Console.ReadKey();            
        }

        static void WriteSync(string entry)
        {
            lock (_syncRoot)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
