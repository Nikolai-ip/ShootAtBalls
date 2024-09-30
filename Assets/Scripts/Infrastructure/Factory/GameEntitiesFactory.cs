using GameCore.Gun;
using GameCore.Projectile;
using Infrastructure.AssetManagement;
using Input;

namespace Infrastructure.Factory
{
    public class GameEntitiesFactory : IGameEntitiesFactory
    {
        private IAssetProvider _assetProvider;
        private IInputHandler _inputHandler;
        private BubbleContainer _projectileContainer;
        public GameEntitiesFactory(IAssetProvider assetProvider, IInputHandler inputHandler)
        {
            _assetProvider = assetProvider;
            _inputHandler = inputHandler;
        }

        public void CreateGameEntities()
        {
            CreateProjectileContainer();
            CreateGun();
        }

        private void CreateProjectileContainer()
        {
            _projectileContainer = _assetProvider
                .Instantiate(AssetPath.BubbleContainerPrefab)
                .GetComponent<BubbleContainer>();
        }

        private void CreateGun()
        {
            var gun = _assetProvider.Instantiate(AssetPath.GunPrefab);
            gun.GetComponent<GunManager>().Init(_inputHandler);
            gun.GetComponent<ShotController>().Init(_projectileContainer);
        }
    }
}