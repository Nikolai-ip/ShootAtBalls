using Infrastructure.AssetManagement;
using Input;
using UnityEngine;

namespace Infrastructure.Factory
{

    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputHandler _inputHandler;

        public GameFactory(IAssetProvider assetProvider, IInputHandler inputHandler)
        {
            _assetProvider = assetProvider;
            _inputHandler = inputHandler;
        }


        private void CreateHud()
        {
            var window = _assetProvider.Instantiate(AssetPath.PrefabWindow, at:Vector3.zero);
            window.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            window.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void CreateView()
        {
        }
        
    }
}