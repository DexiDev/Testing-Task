using System;
using Units.Character;
using Game.Data;
using Game.Data.Implementation.Fields;
using Game.Input;
using UnityEngine;
using Zenject;

namespace Units.Handlers
{
    public class InputHandler : IHandler<IDataController>
    {
        private InputManager _inputManager;
        private DirectionField _directionField;

        [Inject]
        private void Install(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Awake()
        {
            _directionField = _targetData.GetDataField<DirectionField>(true);
        }

        private void OnEnable()
        {
            _inputManager.OnDirectionChanged += OnDirectionChanged;
        }

        private void OnDisable()
        {
            _inputManager.OnDirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector2 value)
        {
            _directionField.SetValue(value);
        }
    }
}