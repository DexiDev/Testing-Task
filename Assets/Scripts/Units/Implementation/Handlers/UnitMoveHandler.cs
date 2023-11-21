using Game.Data;
using Game.Data.Implementation.Fields;
using UnityEngine;

namespace Units.Handlers
{
    public class UnitMoveHandler : IHandler<UnitController>
    {
        [SerializeField] protected float _speed;
        
        private IsMoveField _isMoveField;

        protected virtual void Awake()
        {
            _isMoveField = _targetData.GetDataField<IsMoveField>(true);
        }

        private void Update()
        {
            if (_isMoveField.Value)
            {
               Move();
            }
        }

        protected virtual void Move()
        {
            _targetData.Move(transform.forward * Time.deltaTime * _speed);
        }
    }
}