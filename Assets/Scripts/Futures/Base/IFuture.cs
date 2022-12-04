using System;

namespace Futures.Base
{
    public interface IFuture
    {
        bool IsCancelled { get; }
        bool IsDone { get; }
        bool WasRun { get; }
        IFuture AddListenerOnRun(Action<IFuture> method);
        IFuture AddListener(Action<IFuture> method);
        IFuture AddListenerOnFinalize(Action<IFuture> method);
        void RemoveListenerOnRun(Action<IFuture> method);
        void RemoveListener(Action<IFuture> method);
        void RemoveListenerOnFinalize(Action<IFuture> method);
        void Cancel();
        IFuture Run();
        T Cast<T>() where T : IFuture;
        bool Reuse();
    }
}
