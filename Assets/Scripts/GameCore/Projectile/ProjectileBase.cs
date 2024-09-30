using UnityEngine;

namespace GameCore.Projectile
{
    public abstract class ProjectileBase:MonoBehaviour
    {
        protected Transform Tr;
        protected Vector2? Dir;
        [SerializeField] protected float Speed;

        private void Awake()
        {
            Tr = GetComponent<Transform>();
        }

        public void Run(Vector2 dir)
        {
            Dir = dir;
        }

        private void Update()
        {
            if (Dir!=null)
            {
                Vector3 move = Dir.Value * Speed *Time.deltaTime;
                Tr.position += move;
            }
        }
    }
}