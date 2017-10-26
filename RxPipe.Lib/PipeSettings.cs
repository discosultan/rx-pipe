namespace RxPipe.Lib
{
    public struct PipeSettings
    {
        public int MaxNumParallelProcessings { get; set; }

        public bool IsLimited => MaxNumParallelProcessings > 0;

        public static PipeSettings Default => new PipeSettings
        {
            MaxNumParallelProcessings = 2
        };
    }
}
