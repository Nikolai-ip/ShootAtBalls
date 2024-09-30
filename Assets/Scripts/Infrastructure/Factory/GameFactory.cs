using GameCore.Gun;
using Infrastructure.AssetManagement;
using Input;

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
        
        public void CreateView()
        {
        }
        
        public void CreateControllers()
        {
            var mouseInput = _assetProvider.Instantiate(AssetPath.MouseInputPrefab);
            mouseInput.GetComponent<IInputProvider>().Init(_inputHandler);
        }

    }
}