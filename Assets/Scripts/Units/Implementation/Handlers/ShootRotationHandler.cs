using System;
using Units.Character;
using Game.Data;
using Game.Data.Implementation.Fields;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.Handlers
{
    public class ShootRotationHandler : IHandler<ShootingController>
    {
        [SerializeField] private float _rotationLimit = 90f;
        [SerializeField] private float _rotationSpeed = 15f;
        
        private DirectionField _directionField;
        
        private void Awake()
        {
            _directionField = _targetData.GetDataField<DirectionField>(true);
        }

        private void Update()
        {
            var xDirection = 90 * _directionField.Value.x;
            
            xDirection = Mathf.Clamp(xDirection, -_rotationLimit, _rotationLimit);
            Vector3 targetEuler = Vector3.up * xDirection;
            
            var targetRotation = Quaternion.Euler(targetEuler);
            
            var rotation = Quaternion.Lerp(_targetData.ShootingAim.localRotation, targetRotation,
                Time.deltaTime * _rotationSpeed);
            
            _targetData.SetAimRotation(rotation);
        }
    }
}