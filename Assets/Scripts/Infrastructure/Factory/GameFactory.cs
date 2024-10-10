using System.Collections.Generic;
using GameCore.Field;
using GameCore.GameControllers;
using GameCore.Gun;
using GameCore.Projectile;
using GameCore.Projectile.Container;
using GameCore.UI;
using Infrastructure.AssetManagement;
using Input;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private IInputHandler _inputHandler;
        private IStaticDataService _staticDataService;
        private IProjectileContainer<Bubble> _projectileContainer;
        private ShotController _shotController;
        private Canvas _canvas;
        public GameFactory(IAssetProvider assetProvider, IInputHandler inputHandler, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _inputHandler = inputHandler;
            _staticDataService = staticDataService;
        }


        public void CreateControllers()
        {
            var mouseInput = _assetProvider.Instantiate(AssetPath.MouseInputPrefab);
            mouseInput.GetComponent<IInputProvider>().Init(_inputHandler);
        }
        public void CreateGameEntities()
        {
            CreateProjectileContainer();
            CreateGun();
            CreateBubbleField();
        }
        
        public void CreateView()
        {
            CreateCanvas();
            CreateBubbleIndicators();
            CreateTrajectoryDrawer();
        }

        private void CreateProjectileContainer()
        {
            _projectileContainer = _assetProvider
                .Instantiate(AssetPath.BubbleContainerPrefab)
                .GetComponent<IProjectileContainer<Bubble>>();
        }

        private void CreateGun()
        {
            var gun = _assetProvider.Instantiate(AssetPath.GunPrefab);
            gun.GetComponent<GunManager>().Init(_inputHandler);
            _shotController = gun.GetComponent<ShotController>();
            _shotController.Init(_projectileContainer);
        }

        private void CreateBubbleField()
        {
            var bubbleField = _assetProvider
                .Instantiate(AssetPath.BubbleFieldPrefab)
                .GetComponent<BubbleField>();
            bubbleField.Init(_staticDataService.BubbleFieldData);
        }

        private void CreateCanvas()
        {
            _canvas = _assetProvider.Instantiate(AssetPath.Canvas).GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
        }

        private void CreateBubbleIndicators()
        {

            var gunIndicator = _assetProvider.Instantiate<BubbleIndicatorsController>(AssetPath.BubbleGunIndicatorPrefab);
            var magazineIndicator =
                _assetProvider.Instantiate<BubbleMagazineIndicator>(AssetPath.MagazineIndicatorPrefab);
            magazineIndicator.Init(GetIndicators(),_projectileContainer);
            magazineIndicator.transform.SetParent(_canvas.transform, false);
            
            gunIndicator.Init(GetIndicators(),_shotController);
        }

        private Dictionary<BubbleType, Transform> GetIndicators()
        {
            Dictionary<BubbleType, Transform> indicators = new()
            {
                {
                    BubbleType.Red,
                    _assetProvider.Instantiate(AssetPath.BubbleIndicators[(int) BubbleType.Red]).transform
                },
                {
                    BubbleType.Green,
                    _assetProvider.Instantiate(AssetPath.BubbleIndicators[(int) BubbleType.Green]).transform
                },
                {
                    BubbleType.Blue,
                    _assetProvider.Instantiate(AssetPath.BubbleIndicators[(int) BubbleType.Blue]).transform
                }
            };
            return indicators;
        }


        private void CreateTrajectoryDrawer()
        {
            var trajectoryDrawer = _assetProvider.Instantiate<TrajectoryDrawer>(AssetPath.TrajectoryDrawerPrefab);
            trajectoryDrawer.Init(_shotController.ShotPoint,_inputHandler, _shotController);

        }
    }
}