using System;
using UnityEngine;

namespace Input
{
    public class MouseInputHandler:IInputHandler
    {
        public event Action<Vector2> OnPointMoved;
        public event Action OnPointDown;
        public event Action OnPointUp;
        
        public void Update()
        {
            if (Camera.main != null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                OnPointMoved?.Invoke(mousePos);
            }
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnPointDown?.Invoke();
            if (UnityEngine.Input.GetMouseButtonUp(0))
                OnPointUp?.Invoke();
        }
    }
}