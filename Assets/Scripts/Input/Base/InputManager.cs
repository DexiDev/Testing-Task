using System;
using UnityEngine;

namespace Game.Input
{
    public class InputManager
    {
        private bool _isPressed;
        private Vector2 _direction = Vector2.zero;

        public Vector2 Direction => _direction;
        public bool IsPressed => _isPressed;
        
        public event Action<Vector2> OnDirectionChanged;
        public event Action<bool> OnPressedChanged;
        
        
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
            OnDirectionChanged?.Invoke(direction);
        }

        public void SetPressed(bool isPressed)
        {
            if (_isPressed != isPressed)
            {
                _isPressed = isPressed;
                OnPressedChanged?.Invoke(isPressed);
            }
        }
    }
}