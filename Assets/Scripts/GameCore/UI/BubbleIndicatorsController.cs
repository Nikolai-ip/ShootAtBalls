using System.Collections.Generic;
using GameCore.GameControllers;
using GameCore.Projectile;
using UnityEngine;

namespace GameCore.UI
{
    public class BubbleIndicatorsController:MonoBehaviour
    {
        private const string ShotPoint = "ShotPoint";
        private ShotController _shotController;
        private Transform _indicatorTr;
        private Dictionary<BubbleType,Transform> _indicators;
        private Transform _shotPointTr;
        public void Init(Dictionary<BubbleType,Transform> indicators, ShotController shotController)
        {
            _indicators = indicators;
            _shotController = shotController;
            _shotPointTr = GameObject.FindGameObjectWithTag(ShotPoint).GetComponent<Transform>();
            foreach (var indicator in _indicators)
            {
                indicator.Value.transform.SetParent(transform);
                indicator.Value.gameObject.SetActive(false);
            }
            OnProjectileChanged(shotController.CurrentProjectile);
            _shotController.ProjectileChanged += OnProjectileChanged;
        }

        private void OnDestroy()
        {
            _shotController.ProjectileChanged -= OnProjectileChanged;
        }
        
        private void Update()
        {
            if (_shotPointTr && _indicatorTr)
            {
                _indicatorTr.position = (Vector2)_shotPointTr.position;
            }
        }

        private void OnProjectileChanged(ProjectileBase projectile)
        {
            if (_indicatorTr)
                _indicatorTr.gameObject.SetActive(false);
            
            if (projectile is Bubble bubble)
            {
                if (_indicators.TryGetValue(bubble.Type, out var indicator))
                {
                    _indicatorTr = indicator;
                    _indicatorTr.gameObject.SetActive(true);
                }
            }
        }
    }
}