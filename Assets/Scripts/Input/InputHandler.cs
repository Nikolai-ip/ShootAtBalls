using System;
using UnityEngine;

namespace Input
{
    public class MouseInput: IInputHandler
    {
        public event Action<Vector2> OnPointMoved;

        public void Update()
        {
            if (Camera.main != null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                OnPointMoved?.Invoke(mousePos);
            }
        }
    }
}