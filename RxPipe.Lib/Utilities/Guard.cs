using System;
using System.Diagnostics;

namespace RxPipe.Lib.Utilities
{
    internal static class Guard
    {
        [DebuggerHidden]
        public static void NotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }
    }
}
