using System;
using UnityEngine;

namespace GameCore.Projectile
{
    public class BubbleCollisionDetector:MonoBehaviour
    {
        public event Action<Bubble> OnCollision;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out Bubble bubble))
            {
                OnCollision?.Invoke(bubble);
            }
        }
    }
}