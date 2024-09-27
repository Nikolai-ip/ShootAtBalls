using System;
using System.Collections.Generic;
using Assets.Scripts.Cmd;
using Infrastructure;
using Infrastructure.Cmd;
using Infrastructure.Factory;
using Infrastructure.GameSM;
using Infrastructure.GameSM.GameState;
using Infrastructure.SceneLoader;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;

namespace GameCore
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        public GameStateMachine(ISceneLoader sceneLoader, GameServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                {typeof(BootstrapState), new BootstrapState(this, sceneLoader, services)},
                {typeof(LoadLevelState), new LoadLevelState(this, sceneLoader,services.Single<IGameFactory>())},
                {typeof(GameCycle), new GameCycle(this)},
            };
        }
        public void Enter<TState>() where TState: class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState: class, IPayLoadedState<TPayLoad>
        {
            var state = ChangeState<TState>();
            state.Enter(payLoad);
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            var state = GetState<TState>();
            _currentState = state;
            return state;
        }
        private TState GetState<TState>() where TState: class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}