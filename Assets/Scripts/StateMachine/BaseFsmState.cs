namespace LocalMultiplayer.StateMachine
{
    public abstract class BaseFsmState : IFsmState
    {
        protected Fsm Fsm;
        protected IConfig Config;

        public void Initialize(Fsm fsm, IConfig config)
        {
            Fsm = fsm;
            Config = config;
            InternalInitialize();
        }
        public abstract void InternalInitialize();
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}