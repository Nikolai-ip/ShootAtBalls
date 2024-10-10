using System;
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

        public void Init(IProjectileContainer<Bubble> container)
        {
            _container = container;
            CurrentProjectile = _container.GetProjectile();
            CurrentProjectile.Disable();
        }


        public void Shoot(Vector2 pointPos)
        {
            if (CurrentProjectile)
            {
                CurrentProjectile.Enable();
                CurrentProjectile.transform.position = _shotPoint.position;
                var dir = (pointPos - (Vector2) _shotPoint.position).normalized;
                CurrentProjectile.Run(dir);
                CurrentProjectile = _container.GetProjectile();
                CurrentProjectile.Disable();
            }
        }
        
        
    }
}