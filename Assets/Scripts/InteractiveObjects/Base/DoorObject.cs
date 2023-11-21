using System;
using Sirenix.OdinInspector;
using TMPro;
using Units.Character;
using UnityEngine;
using Zenject;

namespace Game.InteractiveObjects
{
    public class DoorObject : MonoBehaviour
    {
        [SerializeField, TabGroup("Animator")] private Animator _animator;
        [SerializeField, TabGroup("Animator")] private string _isOpenKey = "IsOpen";
        [SerializeField, TabGroup("Parameters")] private float _minDistance = 5f;
        
        private PlayerController _playerController;
        
        [Inject]
        private void Install([Inject(Id = "Player")] PlayerController player)
        {
            _playerController = player;
        }
        
        private void LateUpdate()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, _playerController.transform.position);

            if (distanceToPlayer <= _minDistance)
            {
                SetOpen(true);
            }
        }

        public void SetOpen(bool isOpen)
        {
            if(_animator != null) _animator.SetBool(_isOpenKey, isOpen);
        }
    }
}