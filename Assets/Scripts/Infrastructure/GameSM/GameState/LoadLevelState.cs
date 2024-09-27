using GameCore;
using Infrastructure.Factory;
using Infrastructure.SceneLoader;

namespace Infrastructure.GameSM.GameState
{
    public class LoadLevelState : IPayLoadedState<string>, IState
    {
        private readonly GameStateMachine _gameSm;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameSm = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }
        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void Exit()
        {
        }

        public void Enter()
        {
            OnLoaded();
        }

        private void OnLoaded()
        {
            _gameFactory.CreateView();
            _gameSm.Enter<GameCycle>();
        }
    }
}