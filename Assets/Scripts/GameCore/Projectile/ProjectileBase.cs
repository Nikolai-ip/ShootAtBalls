using System;
using UnityEngine;

namespace GameCore.Projectile
{
    public abstract class ProjectileBase:MonoBehaviour
    {
        protected Transform Tr;
        protected Vector2 Dir;
        [SerializeField] protected float Speed;
        [SerializeField] protected float MaxSpeed;
        private float _originSpeed;
        private SpriteRenderer _sr;
        private Collider2D _col;

        protected virtual void Awake()
        {
            Tr = GetComponent<Transform>();
            _sr = GetComponent<SpriteRenderer>();
            _col = GetComponent<Collider2D>();
            _originSpeed = Speed;
        }

        private void OnDisable()
        {
            Dir = Vector2.zero;
        }
        
        public void Run(Vector2 dir)
        {
            Dir = dir;
        }

        public void ApplySpeedRatio(float speedRatio)
        {
            Speed *= speedRatio;
            Speed = Mathf.Clamp(Speed, _originSpeed, MaxSpeed);
        }
        public void Enable()
        {
            _sr.enabled = true;
            _col.enabled = true;
            Speed = _originSpeed;
        }

        public void Disable()
        {
            _sr.enabled = false;
            _col.enabled = false;
        }
        private void Update()
        {
            Vector3 move = Dir * Speed *Time.deltaTime;
            Tr.position += move;
        }
        
    }
}