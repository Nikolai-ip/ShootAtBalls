using System;
using GameCore.Projectile;
using UnityEngine;

namespace GameCore.Gun
{
    public class ShotController:MonoBehaviour
    {
        private BubbleContainer _container;
        private Transform _tr;
        [SerializeField] private Transform _shotPoint;
        
        public void Init(BubbleContainer container)
        {
            _container = container;
            _tr = GetComponent<Transform>();
        }

        public void Shoot(Vector2 pointPos)
        {
            var projectile = _container.GetProjectile();
            projectile.transform.position = _shotPoint.position;
            var dir = (pointPos - (Vector2) _tr.position).normalized;
            projectile.Run(dir);
        }
        
    }
}