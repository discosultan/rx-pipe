using System.Threading;

namespace RxPipe.Lib.Utilities
{
    internal class OptionalSemaphore
    {
        private readonly int _timeout;
        private readonly Semaphore _semaphore;

        public OptionalSemaphore(int count, int timeout)
        {
            _timeout = timeout;
            if (count != Timeout.Infinite)
            {
                _semaphore = new Semaphore(count, count);
            }
        }

        public bool Enabled => _semaphore != null;

        public int Release()
        {
            return !Enabled ? 0 : _semaphore.Release();
        }

        public bool WaitOne()
        {
            return !Enabled || _semaphore.WaitOne(_timeout);
        }
    }
}
