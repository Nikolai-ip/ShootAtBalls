using System;
using System.Collections.Generic;
using GameCore.Projectile;
using GameCore.Projectile.Container;
using TMPro;
using UnityEngine;

namespace GameCore.UI
{
    public class BubbleMagazineIndicator : MonoBehaviour
    {
        private IProjectileContainer<Bubble> _container;
        private Dictionary<BubbleType, Transform> _indicators;
        private Transform _indicator;
        [SerializeField] private TextMeshProUGUI _magazineCapacityUI;
        private int _capacity;

        public void Init(Dictionary<BubbleType, Transform> indicators, IProjectileContainer<Bubble>  container)
        {
            _container = container;
            _indicators = indicators;
            foreach (var kvp in _indicators)
            {
                kvp.Value.transform.SetParent(transform,false);
                kvp.Value.gameObject.SetActive(false);
            }
            OnCapacityChanged(container.MagazineCapacity);
            OnNextProjectileChanged(container.NextProjectile);
            _container.NextProjectileChanged += OnNextProjectileChanged;
            _container.MagazineCapacityChanged += OnCapacityChanged;
        }
    
        private void OnDestroy()
        {
            _container.NextProjectileChanged -= OnNextProjectileChanged;
            _container.MagazineCapacityChanged -= OnCapacityChanged; 
        }
        private void OnCapacityChanged(int capacity)
        {
            _capacity = capacity;
            _magazineCapacityUI.text = _capacity.ToString();
        }

        private void OnNextProjectileChanged(Bubble bubble)
        {
            if (_indicator)
                _indicator.gameObject.SetActive(false);
            if (_capacity == 0) return;
            if (_indicators.TryGetValue(bubble.Type, out var indicator))
            {
                _indicator = indicator;
                _indicator.gameObject.SetActive(true);
            }
        }
        
    }
}