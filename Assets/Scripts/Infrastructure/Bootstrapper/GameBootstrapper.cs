using Infrastructure.GameSM.GameState;
using UnityEngine;

namespace Infrastructure.Bootstrapper
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private static Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}