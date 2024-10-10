using System;
using System.Collections.Generic;

namespace GameCore.Projectile.Container
{
    public interface IProjectileContainer<TProjectile>
    {
        TProjectile GetProjectile();
        int MagazineCapacity { get; }
        TProjectile NextProjectile { get; }
        event Action<int> MagazineCapacityChanged;
        event Action<TProjectile> NextProjectileChanged ;
    }
}