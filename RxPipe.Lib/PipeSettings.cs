using System.Threading;

namespace RxPipe.Lib
{
    public struct PipeSettings
    {
        public int MaxNumParallelProcessings { get; set; }
        public int WaitForProviderMs { get; set; }

        public static PipeSettings Default => new PipeSettings
        {
            WaitForProviderMs = Timeout.Infinite,
            MaxNumParallelProcessings = Timeout.Infinite
        };
    }
}
