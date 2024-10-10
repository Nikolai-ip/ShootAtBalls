using System;
using GameCore.Screen;
using UnityEngine;

namespace GameCore.GameControllers
{
    public class ScreenBoundaryCollisionDetector : MonoBehaviour
    {
        private float _leftBoundary;
        private float _rightBoundary;
        private float _topBoundary;
        private Transform _transform;
        [SerializeField] private Vector2 _objectSize;
        [SerializeField] private float _collisionThreshold = 0.1f;
        private bool _isCollisionDetectionEnabled = true; 
        public event Action<ScreenBoundary> OnScreenBoundaryCollision;

        private void Awake()
        {
            SetScreenBoundaries();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_isCollisionDetectionEnabled)
            {
                DetectBoundaryCollision(_leftBoundary, _transform.position.x, ScreenBoundary.Left);
                DetectBoundaryCollision(_rightBoundary, _transform.position.x, ScreenBoundary.Right);
                DetectBoundaryCollision(_topBoundary, _transform.position.y, ScreenBoundary.Top);
            }
        }

        private void SetScreenBoundaries()
        {
            Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            _leftBoundary = screenBottomLeft.x+_objectSize.x/2;
            _rightBoundary = screenTopRight.x-_objectSize.x/2;
            _topBoundary = screenTopRight.y-_objectSize.y/2;
        }

        private void DetectBoundaryCollision(float boundary, float position, ScreenBoundary boundarySide)
        {
            if (IsCollisionDetected(boundary, position))
            {
                _isCollisionDetectionEnabled = false;
                OnScreenBoundaryCollision?.Invoke(boundarySide);
            }
            else
            {
                _isCollisionDetectionEnabled = true;
            }
        }

        private bool IsCollisionDetected(float boundary, float position)
        {
            return Mathf.Abs(boundary - position) < _collisionThreshold; 
        }


    }
}
