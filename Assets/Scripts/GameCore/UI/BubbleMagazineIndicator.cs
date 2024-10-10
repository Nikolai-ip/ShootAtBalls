using System.Collections.Generic;
using GameCore.Projectile;
using GameCore.Projectile.Container;
using UnityEngine;

namespace GameCore.UI
{
    public class BubbleMagazineIndicator : MonoBehaviour
    {
        private IProjectileContainer<Bubble> _container;
        private Dictionary<BubbleType, Transform> _indicators;

        public void Init(Dictionary<BubbleType, Transform> indicators, IProjectileContainer<Bubble>  container)
        {
            _container = container;
            _container.MagazineChanged += OnMagazineChanged;
            _indicators = indicators;
            foreach (var kvp in _indicators)
            {
                kvp.Value.transform.SetParent(transform);
            }

            DisableIndicators();
        }

        private void DisableIndicators()
        {
            foreach (var kvp in _indicators)
            {
                kvp.Value.gameObject.SetActive(false);
            }
        }
        private void OnMagazineChanged(IEnumerable<Bubble> projectiles)
        {
            DisableIndicators();
            foreach (var bubble in projectiles)
            {
                _indicators[bubble.Type].gameObject.SetActive(true);
            }
        }
    }
}