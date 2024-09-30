using System;
using Infrastructure.Services;
using UnityEngine;

namespace Input
{
    public interface IInputHandler:IService
    {
        event Action<Vector2> OnPointMoved;
        event Action OnPointDown;
        event Action OnPointUp;
        void Update();
    }
    
}