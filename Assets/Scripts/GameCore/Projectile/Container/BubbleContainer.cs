using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Infrastructure.PoolObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.Projectile.Container
{
    public class BubbleContainer:MonoBehaviour, IProjectileContainer<Bubble>
    {
        [SerializeField] private int _capacity;
        [SerializeField] private List<Bubble> _prefabs; 
        private List<PoolMono<Bubble>> _containers = new();

        public int MagazineCapacity => _countOfSubsequentProjectiles;

        [SerializeField] private int _countOfSubsequentProjectiles;
        
        public event Action<IEnumerable<Bubble>> MagazineChanged;

        private void Awake()
        {
            for (int i = 0; i < _prefabs.Count; i++)
            {
                _containers.Add(new PoolMono<Bubble>(_prefabs[i],_capacity,transform){AutoExpand = true});
            }

        }
        
        public Bubble GetProjectile()
        {
            return GetRandomProjectile();
        }


        private Bubble GetRandomProjectile()
        {
            var container = _containers[Random.Range(0, _containers.Count)];
            return container.GetFreeElement();
        }
        private IEnumerable<Bubble> GetRandomProjectiles()
        {
            var projectiles = new Collection<Bubble>();
            for (int i = 0; i < _countOfSubsequentProjectiles; i++)
            {
                projectiles.Add(GetRandomProjectile());
            }

            return projectiles;
        }
    }
    
}