using UnityEngine;

namespace GameCore.Gun
{
    public class RotateController:MonoBehaviour
    {
        private Transform _tr;
        [SerializeField] private float _offsetAngle;
        private void Awake()
        {
            _tr = GetComponent<Transform>();
        }

        public void Rotate(Vector2 at)
        {
            Vector2 dir = (at - (Vector2)_tr.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _tr.eulerAngles = new Vector3(0, 0, angle+_offsetAngle);
        }
    }
}