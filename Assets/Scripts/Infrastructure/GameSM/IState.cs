using Assets.Scripts.Cmd;

namespace Infrastructure.GameSM
{
    public interface IState:IExitableState
    {
        void Enter();
    }
    public interface IPayLoadedState<TPayload>:IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}