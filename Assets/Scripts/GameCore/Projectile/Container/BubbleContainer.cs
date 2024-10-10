using System;
using System.Collections.Generic;
using Infrastructure.PoolObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.Projectile.Container
{
    public class BubbleContainer:MonoBehaviour, IProjectileContainer<Bubble>
    { 
        [SerializeField] private int _containerCapacity;
        [SerializeField] private List<Bubble> _prefabs; 
        private List<PoolMono<Bubble>> _containers = new();
        [SerializeField] private int _magazineCapacity;
        public int MagazineCapacity 
        { 
            get=> _magazineCapacity;
            set
            {
                _magazineCapacity = value;
                MagazineCapacityChanged?.Invoke(_magazineCapacity);
            }
        }
        public event Action<int> MagazineCapacityChanged;
        public event Action<Bubble> NextProjectileChanged;
        private Bubble _nextProjectile;

        public Bubble NextProjectile
        {
            get => _nextProjectile;
            set
            {
                _nextProjectile = value;
                NextProjectileChanged?.Invoke(_nextProjectile);
            } 
        }

        private void Awake()
        {
            for (int i = 0; i < _prefabs.Count; i++)
            {
                _containers.Add(new PoolMono<Bubble>(_prefabs[i],_containerCapacity,transform){AutoExpand = true});
            }

            NextProjectile = GetRandomProjectile();
            NextProjectileChanged?.Invoke(NextProjectile);
            MagazineCapacityChanged?.Invoke(_magazineCapacity);
            NextProjectile.Disable();
        }
        public Bubble GetProjectile()
        {
            if (MagazineCapacity == 0)
                return null;
            MagazineCapacity--;
            
            if (NextProjectile != null)
            {
                var projectile =  NextProjectile;
                projectile.Enable();
                NextProjectile = GetRandomProjectile();
                NextProjectile.Disable();
                return projectile;
            }
            return GetRandomProjectile();
        }
        
        private Bubble GetRandomProjectile()
        {
            var container = _containers[Random.Range(0, _containers.Count)];
            return container.GetFreeElement();
        }
    }
    
}