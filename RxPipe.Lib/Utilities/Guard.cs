using System;
using System.Diagnostics;

namespace RxPipe.Lib.Utilities
{
    static class Guard
    {
        [DebuggerHidden]
        internal static void NotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }
    }
}
