using Infrastructure.PoolObject;
using UnityEngine;

namespace GameCore.Projectile
{
    public class BubbleContainer:MonoBehaviour
    {
        private PoolMono<ProjectileBase> _container;
        [SerializeField] private ProjectileBase _prefab;
        [SerializeField] private int _capacity;
        [SerializeField] private bool _autoExpand;
        private void Awake()
        {
            _container = new PoolMono<ProjectileBase>(_prefab, _capacity, transform)
            {
                AutoExpand = _autoExpand
            };
        }

        public ProjectileBase GetProjectile()
        {
            return _container.GetFreeElement();
        }
    }
    
}