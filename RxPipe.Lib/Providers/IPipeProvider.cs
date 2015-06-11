using System;

namespace RxPipe.Lib.Providers
{
    public interface IPipeProvider<out T>
    {
        IObservable<T> GetAll();
    }
}