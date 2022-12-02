namespace LocalMultiplayer.StateMachine
{
    public interface IFsmState : IUpdateListener
    {
        void Initialize(Fsm fsm, IConfig config);
        void Enter();
        void Exit();
    }
}