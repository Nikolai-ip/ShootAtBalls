using GameCore;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;

namespace Infrastructure
{
    internal class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), GameServices.Container);
        }
    }
}