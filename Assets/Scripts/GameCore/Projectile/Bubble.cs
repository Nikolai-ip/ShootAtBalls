using System;
using GameCore.GameControllers;
using GameCore.Screen;
using UnityEngine;

namespace GameCore.Projectile
{
    [RequireComponent(typeof(ScreenBoundaryCollisionDetector))]
    [RequireComponent(typeof(BubbleCollisionDetector))]
    [RequireComponent(typeof(BubbleBurstController))]
    [RequireComponent(typeof(BubbleReplacer))]
    public class Bubble : ProjectileBase
    {
        private ScreenBoundaryCollisionDetector _collisionDetector;
        private BubbleCollisionDetector _bubbleCollisionDetector;
        private BubbleBurstController _burstController;
        private BubbleReplacer _bubbleReplacer;
        [SerializeField] private BubbleType _type;
        [SerializeField] private float _angleCollisionToReplace;
        public BubbleType Type => _type;
        protected override void Awake()
        {
            base.Awake();
            _collisionDetector = GetComponent<ScreenBoundaryCollisionDetector>();
            _bubbleCollisionDetector = GetComponent<BubbleCollisionDetector>();
            _burstController = GetComponent<BubbleBurstController>();
            _bubbleReplacer = GetComponent<BubbleReplacer>();
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

        private void OnBubbleCollision(Bubble bubble)
        {
            if (Dir != Vector2.zero)
            {
                float angleCollided = Vector2.Angle(Dir, (bubble.Tr.position - Tr.position).normalized);
                Debug.Log(angleCollided);
                Debug.Log(Speed);
                if (angleCollided < _angleCollisionToReplace && Math.Abs(Speed - MaxSpeed) < 0.01f)
                    _bubbleReplacer.ReplaceBubble(bubble);
                else
                    _burstController.StartBurst();
                
                
            }
            Dir = Vector2.zero;
            
        }
        
    }
}