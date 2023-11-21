using Cinemachine;
using Game.Data;
using Game.Input;
using Units.Character;
using UnityEngine;
using Zenject;

namespace Units.Handlers.Player
{
    public class CameraHandler : IHandler<PlayerController>
    {
        [SerializeField] private CinemachineVirtualCamera _cameraMove;

        private InputManager _inputManager;

        [Inject]
        private void Install(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void OnEnable()
        {
            OnDirectionChanged(_inputManager.Direction);
            _inputManager.OnDirectionChanged += OnDirectionChanged;
        }

        private void OnDisable()
        {
            _inputManager.OnDirectionChanged -= OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector2 direction)
        {
            _cameraMove.gameObject.SetActive(direction != Vector2.zero);
        }
    }
}