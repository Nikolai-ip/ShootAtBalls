using System;
using GameCore.GameControllers;
using GameCore.Screen;
using UnityEngine;

namespace GameCore.Projectile
{
    [RequireComponent(typeof(ScreenBoundaryCollisionDetector))]
    [RequireComponent(typeof(BubbleCollisionDetector))]

    public class Bubble : ProjectileBase
    {
        private ScreenBoundaryCollisionDetector _collisionDetector;
        private BubbleCollisionDetector _bubbleCollisionDetector;
        private BubbleBurstController _burstController;
        [SerializeField] private BubbleType _type;
        public BubbleType Type => _type;
        protected override void Awake()
        {
            base.Awake();
            _collisionDetector = GetComponent<ScreenBoundaryCollisionDetector>();
            _bubbleCollisionDetector = GetComponent<BubbleCollisionDetector>();
            _burstController = GetComponent<BubbleBurstController>();
            _burstController.Init(Type);
            _collisionDetector.OnScreenBoundaryCollision += OnWallCollision;
            _bubbleCollisionDetector.OnCollision += OnBubbleCollision;
        }

        private void OnDestroy()
        {
            _collisionDetector.OnScreenBoundaryCollision -= OnWallCollision;
            _bubbleCollisionDetector.OnCollision -= OnBubbleCollision;
        }

        private void OnWallCollision(ScreenBoundary side)
        {
            Dir = side == ScreenBoundary.Left || side == ScreenBoundary.Right? new Vector2(-Dir.x, Dir.y) : Vector2.zero;
            
        }

        private void OnBubbleCollision(Bubble obj)
        {
            if (Dir != Vector2.zero)
                _burstController.StartBurst();
            Dir = Vector2.zero;
            
        }
        
    }
}