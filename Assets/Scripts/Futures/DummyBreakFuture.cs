﻿using Futures.Base;

namespace Futures
{
    public class DummyBreakFuture : Future
    {

        protected override void OnRun()
        {
            Complete();
        }

        protected override void OnComplete()
        {
        }
    }
}
