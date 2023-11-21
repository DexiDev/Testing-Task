using System;
using Game.Data;
using Game.Data.Implementation.Fields;
using Units.Shotings;
using UnityEngine;

namespace Units.Handlers.Bullet
{
    public class BulletSizeHandler : IHandler<BulletController>
    {
        [SerializeField] private float _speedSizing;

        private IsMoveField _isMoveField;
        
        private void Awake()
        {
            _isMoveField = _targetData.GetDataField<IsMoveField>();
        }

        private void OnEnable()
        {
            _targetData.SetScale(_targetData.DefaultScale);
        }

        private void Update()
        {
            if (!_isMoveField.Value)
            {
                var scale = Vector3.one * Time.deltaTime * _speedSizing;
                var targetScale = _targetData.transform.localScale + scale; 
                _targetData.SetScale(targetScale);
            }
        }
    }
}