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
        [SerializeField] private float _extraPadding = 0.05f; // Дополнительный запас для границ
        private Vector2 _previousPosition;  // Предыдущее положение объекта
        private bool _isCollisionDetectionEnabled = true; 
        public event Action<ScreenBoundary> OnScreenBoundaryCollision;

        private void Awake()
        {
            SetScreenBoundaries();
            _transform = GetComponent<Transform>();
            _previousPosition = _transform.position;  // Инициализация предыдущей позиции
        }

        private void Update()
        {
            if (_isCollisionDetectionEnabled)
            {
                // Рассчитываем изменения по осям
                float deltaX = _transform.position.x - _previousPosition.x;
                float deltaY = _transform.position.y - _previousPosition.y;

                // Проверяем столкновения
                DetectBoundaryCollision(_leftBoundary, _transform.position.x, deltaX, ScreenBoundary.Left);
                DetectBoundaryCollision(_rightBoundary, _transform.position.x, deltaX, ScreenBoundary.Right);
                DetectBoundaryCollision(_topBoundary, _transform.position.y, deltaY, ScreenBoundary.Top);

                _previousPosition = _transform.position;  // Обновляем предыдущую позицию
            }
        }

        private void SetScreenBoundaries()
        {
            Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            
            // Увеличиваем/уменьшаем границы с учетом размера объекта и дополнительного запаса
            _leftBoundary = screenBottomLeft.x + _objectSize.x / 2 + _extraPadding;
            _rightBoundary = screenTopRight.x - _objectSize.x / 2 - _extraPadding;
            _topBoundary = screenTopRight.y - _objectSize.y / 2 - _extraPadding;
        }

        private void DetectBoundaryCollision(float boundary, float position, float deltaPosition, ScreenBoundary boundarySide)
        {
            // Проверяем текущее и будущее положение на предмет столкновения
            float futurePosition = position + deltaPosition;

            if (IsCollisionDetected(boundary, position) || IsCollisionDetected(boundary, futurePosition))
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
