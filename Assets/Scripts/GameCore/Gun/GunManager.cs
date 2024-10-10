using GameCore.GameControllers;
using Input;
using UnityEngine;

namespace GameCore.Gun
{
    [RequireComponent(typeof(ShotController))]
    [RequireComponent(typeof(RotateController))]

    public class GunManager:MonoBehaviour
    {
        private IInputHandler _inputHandler;
        private RotateController _rotateController;
        private ShotController _shotController;
        private Vector2 _pointPos;
        public void Init(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.OnPointMoved += OnPointMoved;
            _inputHandler.OnPointUp += OnPointUp;
            _inputHandler.OnPointDown += OnPointDown;
            _rotateController = GetComponent<RotateController>();
            _shotController = GetComponent<ShotController>();
        }

        private void OnDestroy()
        {
            _inputHandler.OnPointMoved -= OnPointMoved;
            _inputHandler.OnPointUp -= OnPointUp;
        }

        private void OnPointMoved(Vector2 position)
        { 
            _rotateController.Rotate(at:position);
            _pointPos = position;
        }

        private void OnPointDown()
        {
            _shotController.StartAccumSpeed();
        }

        private void OnPointUp()
        {
            _shotController.Shoot(_pointPos);
        }
    }
}