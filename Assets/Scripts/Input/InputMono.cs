using UnityEngine;

namespace Input
{
    public class InputMono:MonoBehaviour,IInputProvider
    {
        private IInputHandler _inputHandler;
        public void Init(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        private void Update()
        {
            _inputHandler.Update();
        }
    }
}