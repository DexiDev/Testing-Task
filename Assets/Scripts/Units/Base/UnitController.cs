using System;
using Game.Assets;
using Game.Data;
using Game.Data.Implementation.Fields;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units
{
    [DisallowMultipleComponent]
    public class UnitController : DataController, IUnit
    {
        [SerializeField, TabGroup("Components")] protected Animator _animator;
        [SerializeField, TabGroup("Animation Key")] private string _isMovingKey = "IsMoving";
        
        protected IsMoveField _isMoveField;
        
        public Transform Transform => transform;

        public GameObject Asset => gameObject;
        public event Action<IAsset> OnReleased;
        
        protected virtual void Awake()
        {
            _isMoveField = GetDataField<IsMoveField>(true);
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            OnReleased?.Invoke(this);
        }

        public virtual void Move(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            transform.position += direction;
        }

        public virtual void SetRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }
        
        public virtual void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        protected virtual void SetMoving(bool isMoving)
        {
            if(_animator != null) _animator.SetBool(_isMovingKey, isMoving);
        }
    }
}