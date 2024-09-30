﻿using GameCore;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.SceneLoader;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Input;

namespace Infrastructure.GameSM.GameState
{
    public class BootstrapState:IState
    {
        private const string InitialScene  = "Initial";
        private const string MainScene = "Main";
        private readonly GameStateMachine _gameSm;
        private readonly ISceneLoader _sceneLoader;
        private GameServices _services;

        public BootstrapState(GameStateMachine gameSm, ISceneLoader sceneLoader, GameServices gameServices)
        {
            _gameSm = gameSm;
            _sceneLoader = sceneLoader;
            _services = gameServices;
            RegisterServices();
        }
        public void Enter()
        {
            if (_sceneLoader.CurrentScene == MainScene)
            {
                _gameSm.Enter<LoadLevelState>();
            }
            else
            {
                _sceneLoader.Load(InitialScene, EnterLoadLevel);

            }
        }

        private void EnterLoadLevel()
        {
            _gameSm.Enter<LoadLevelState, string>(MainScene);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputHandler>(new MouseInputHandler());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IInputHandler>()));
            _services.RegisterSingle<IGameEntitiesFactory>(new GameEntitiesFactory(_services.Single<IAssetProvider>(), _services.Single<IInputHandler>()));

        }

        public void Exit()
        {
        }
    }
}