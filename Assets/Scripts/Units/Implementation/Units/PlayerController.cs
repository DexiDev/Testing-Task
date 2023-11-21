using System;
using Game.Data.Implementation.Fields;
using Game.Input;
using Game.InteractiveObjects;
using Game.Levels;
using UnityEngine;
using Zenject;

namespace Units.Character
{
    [DisallowMultipleComponent]
    public class PlayerController : ShootingController
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private float _factorScale;
        [SerializeField] private float _minScale = 0.2f;
        
        private InputManager _inputManager;
        private IsMoveField _isMoveField;
        private MovePointField _movePointField;
        
        [Inject]
        private void Install(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        protected override void Awake()
        {
            base.Awake();
            _isMoveField = GetDataField<IsMoveField>(true);
            _movePointField = GetDataField<MovePointField>(true);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _isMoveField.OnChanged += OnIsMoveChanged;
            _inputManager.OnPressedChanged += OnPressedChanged;
            
            OnIsMoveChanged(_isMoveField.Value);
            
            _movePointField.SetValue(_levelController.transform.position);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _isMoveField.OnChanged -= OnIsMoveChanged;
            _inputManager.OnPressedChanged -= OnPressedChanged;
        }

        private void OnIsMoveChanged(bool isMove)
        {
            SetMoving(isMove);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out ExploreObject _))
            {
                _isMoveField.SetValue(false);
                gameObject.SetActive(false);
                _levelController.FailedLevel();
            }
        }

        private void OnPressedChanged(bool isPressed)
        {
            if (isPressed) GetBullet();
            else
            {
                var factorScaleBullet =  _bullet.transform.localScale.y / _bullet.DefaultScale.y - 1f;

                var targetScale = transform.localScale - Vector3.one * factorScaleBullet * _factorScale;

                SetScale(targetScale);
                
                Shoot();
            }
            
            SetAimActive(isPressed);
        }

        public override void SetScale(Vector3 scale)
        {
            if (scale.y <= _minScale)
            {
                scale = Vector3.one * _minScale;
                
                _levelController.FailedLevel();
            }

            base.SetScale(scale);
        }

        public void LateUpdate()
        {
            if (transform.position == _levelController.transform.position)
            {
                _isMoveField.SetValue(false);
                _levelController.CompletedLevel();
            }
        }
    }
}