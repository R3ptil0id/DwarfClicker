using Futures.Util;

namespace Futures.Base
{
    public class FutureScenarioFuture : Future
    {
        private readonly FutureScenario _futureScenario;

        public FutureScenarioFuture(FutureScenario futureScenario)
        {
            _futureScenario = futureScenario;
        }
        protected override void OnRun()
        {
            if (_futureScenario.IsEmpty)
            {
                Complete();
                return;
            }

            _futureScenario.Completed += result =>
            {
                if (!result)
                    Complete();
                else
                    Cancel();
            };

            _futureScenario.Run();
        }

        protected override void OnComplete()
        {
            if (IsCancelled)
                _futureScenario.Cancel();
        }
    }
}
