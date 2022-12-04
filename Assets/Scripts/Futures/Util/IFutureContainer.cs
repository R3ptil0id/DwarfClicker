using Futures.Base;

namespace Futures.Util
{
    public interface IFutureContainer
    {
        void AddFuture(IFuture future);
    }
}