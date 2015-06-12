using System;
using System.Threading;
using RxPipe.Lib.Utilities;

namespace RxPipe.ConsoleUI
{
    public class ConsoleLogger : ILogger
    {
        private readonly object _syncRoot;        

        public ConsoleLogger(object syncRoot)
        {
            _syncRoot = syncRoot;
        }

        public ConsoleColor Color { get; set; }

        public void Write(string entry)
        {
            lock (_syncRoot)
            {
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = Color;
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | {entry}");
                Console.ForegroundColor = currentColor;
            }
        }
    }
}
