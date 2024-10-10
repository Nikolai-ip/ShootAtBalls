using System;
using System.Collections.Generic;

namespace GameCore.Projectile.Container
{
    public interface IProjectileContainer<TProjectile>
    {
        TProjectile GetProjectile();
        int MagazineCapacity { get; }
        event Action<IEnumerable<TProjectile>> MagazineChanged ;
    }
}