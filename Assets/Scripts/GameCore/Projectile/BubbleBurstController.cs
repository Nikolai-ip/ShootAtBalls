using System;
using UnityEngine;

namespace GameCore.Projectile
{
    public class BubbleBurstController:MonoBehaviour
    {
        private const int MaxBubbleCollisions = 6;
        [SerializeField] private float _additionalRadius;
        private CircleCollider2D _collider;
        private float _searchRadius;
        private Transform _tr;
        private bool _isBurst;

        private BubbleType _type;
        public void Init(BubbleType type)
        {
            _tr = GetComponent<Transform>();
            _collider = GetComponent<CircleCollider2D>();
            _type = type;
            _searchRadius = _collider.radius/2 + _additionalRadius;
        }

        public bool StartBurst(bool accum = false, BubbleType? bubbleType = null)
        {
            // Если первый шарик уже взорван или его цвет не совпадает, завершить
            if (_isBurst || (bubbleType != null && bubbleType.Value != _type))
                return false;
            _isBurst = true;
            bool foundMatchingBubble = false; // Флаг для отслеживания совпадений

            Collider2D[] colliders = new Collider2D[MaxBubbleCollisions];
            Physics2D.OverlapCircleNonAlloc(_tr.position, _searchRadius, colliders);
            foreach (var collider in colliders)
            {
                if (collider && collider.TryGetComponent(out BubbleBurstController bubble))
                {
                    if (bubble.StartBurst(true, _type))
                    {
                        foundMatchingBubble = true;
                    }
                }
            }
            
            if (foundMatchingBubble || accum)
            {
                Burst();
                return true;
            }
            _isBurst = false;
            return false;
        }

        private void Burst()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _isBurst = false;
        }

        private void OnDrawGizmos()
        {
            if (_collider)
                Gizmos.DrawWireSphere(transform.position,_collider.radius/2  + _additionalRadius);
        }
    }
}