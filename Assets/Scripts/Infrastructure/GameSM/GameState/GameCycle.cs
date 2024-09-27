using GameCore;

namespace Infrastructure.GameSM.GameState
{
    public class GameCycle : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private GameStateMachine _game;
        public GameCycle(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
        
    }
}
