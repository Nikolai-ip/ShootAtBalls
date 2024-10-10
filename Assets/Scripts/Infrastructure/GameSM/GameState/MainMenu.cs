using GameCore;
using GameCore.GameControllers;
using Infrastructure.SceneLoader;
using UnityEngine;

namespace Infrastructure.GameSM.GameState
{
    public class MainMenu:IPayLoadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private ButtonsController _buttonsController;
        private readonly GameStateMachine _gameSM;

        public MainMenu(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameSM = gameStateMachine;
           
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnMenuLoaded);
        }

        private void OnMenuLoaded()
        {
            _buttonsController = GameObject.FindObjectOfType<ButtonsController>();
            _buttonsController.ButtonClicked += OnActionButtonClicked;
        }
        private void OnActionButtonClicked(string action)
        {
            if (action == "Quit")
                Application.Quit();
            if (action == "ToGame")
                _gameSM.Enter<LoadLevelState,string>("Main");
        }
        public void Exit()
        {
            _buttonsController.ButtonClicked -= OnActionButtonClicked;

        }
    }
}