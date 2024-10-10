using System;
using UnityEngine;

namespace GameCore.GameControllers
{
    public class ButtonsController:MonoBehaviour
    {
        public event Action<string> ButtonClicked;
        
        public void OnButtonClicked(string sceneName)
        {
            ButtonClicked?.Invoke(sceneName);
        }
    }
}