using Infrastructure.PoolObject;
using UnityEngine;

namespace GameCore.Projectile
{
    public class ProjectileContainer:MonoBehaviour
    {
        private PoolMono<ProjectileBase> _container;

        public void Init(ProjectileBase prefab, int capacity, bool autoExpand = false)
        {
            _container = new PoolMono<ProjectileBase>(prefab, capacity, transform)
            {
                AutoExpand = autoExpand
            };
        }
    }
    
}