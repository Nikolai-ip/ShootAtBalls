using GameCore;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.SceneLoader;
using Infrastructure.Services.ServiceLocator;
using Input;
using StaticData;
using UnityEngine;

namespace Infrastructure.GameSM.GameState
{
    public class BootstrapState:IState
    {
        private const string InitialScene  = "Initial";
        private const string MainScene = "Main"; 
        private const string MainMenu = "Menu";
        private readonly GameStateMachine _gameSm;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameServices _services;

        public BootstrapState(GameStateMachine gameSm, ISceneLoader sceneLoader, GameServices gameServices)
        {
            _gameSm = gameSm;
            _sceneLoader = sceneLoader;
            _services = gameServices;
            RegisterServices();
            Application.targetFrameRate = 60;
        }
        public void Enter()
        {
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _gameSm.Enter<MainMenu, string>(MainMenu);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputHandler>(new MouseInputHandler());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.Single<IStaticDataService>().LoadGameFieldData();
            _services.RegisterSingle<IGameFactory>
            (new GameFactory(
                    _services.Single<IAssetProvider>(),
                    _services.Single<IInputHandler>(), 
                    _services.Single<IStaticDataService>()));

        }

        public void Exit()
        {
        }
    }
}