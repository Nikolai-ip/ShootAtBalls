using System;
using UnityEngine;

namespace Input
{
    public class ScreenTouchInputController:MonoBehaviour,IInputHandler
    {
        public event Action<Vector2> OnPointMoved;
    }
}