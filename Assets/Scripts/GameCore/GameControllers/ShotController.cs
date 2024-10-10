using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Projectile;
using GameCore.Projectile.Container;
using UnityEngine;

namespace GameCore.GameControllers
{
    public class ShotController:MonoBehaviour
    {
        private IProjectileContainer<Bubble> _container;
        [SerializeField] private Transform _shotPoint;
        private ProjectileBase _currentProjectile;
        public event Action<ProjectileBase> ProjectileChanged;
        public ProjectileBase CurrentProjectile
        {
            get => _currentProjectile;
            private set
            {
                _currentProjectile = value;
                ProjectileChanged?.Invoke(_currentProjectile);
            }
        }

        public Transform ShotPoint => _shotPoint;
        private float _speedRatio = 1;
        public float SpeedRatio => _speedRatio;

        public float MaxSpeedRatio => _maxSpeedRatio;
        public float MaxScatterAngle => _maxScatterAngle;
        
        [SerializeField] private float _maxSpeedRatio;

        [SerializeField] private float _stepAccum;
        private ScatterCalculator _scatterCalculator = new();
        [SerializeField] private float _maxScatterAngle;
            
        public void Init(IProjectileContainer<Bubble> container)
        {
            _container = container;
            CurrentProjectile = _container.GetProjectile();
            CurrentProjectile?.Disable();
        }

        public void StartAccumSpeed()
        {
            StartCoroutine(AccumSpeed());
        }

        private IEnumerator AccumSpeed()
        {
            var delay = new WaitForSeconds(_stepAccum);
            _speedRatio = 1;
            while (true)
            {
                _speedRatio += Time.deltaTime;
                _speedRatio = Mathf.Clamp(_speedRatio, 1, _maxSpeedRatio);
                yield return delay;
            }
        }
        public void Shoot(Vector2 pointPos)
        {
            StopAllCoroutines();
            
            if (CurrentProjectile)
            {
                CurrentProjectile.Enable();
                CurrentProjectile.transform.position = _shotPoint.position;
                var dir = (pointPos - (Vector2) _shotPoint.position).normalized;
                dir = _scatterCalculator.ApplyScatter(dir,_speedRatio,_maxSpeedRatio,_maxScatterAngle);
                CurrentProjectile.Run(dir);
                CurrentProjectile.ApplySpeedRatio(_speedRatio);
                CurrentProjectile = _container.GetProjectile();
                CurrentProjectile?.Disable();
            }
            _speedRatio = 1;
        }

        
    }
}