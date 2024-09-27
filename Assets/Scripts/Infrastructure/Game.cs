using GameCore;
using Infrastructure.SceneLoader;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;

namespace Infrastructure
{
    internal class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoaderService(coroutineRunner), GameServices.Container);
        }
    }
}