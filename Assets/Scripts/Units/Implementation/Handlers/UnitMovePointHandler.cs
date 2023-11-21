using Game.Data;
using Game.Data.Implementation.Fields;
using UnityEngine;

namespace Units.Handlers
{
    public class UnitMovePointHandler : UnitMoveHandler
    {
        private MovePointField _movePointField;

        protected override void Awake()
        {
            base.Awake();
            _movePointField = _targetData.GetDataField<MovePointField>(true);
        }

        protected override void Move()
        {
            if(_movePointField.Value == Vector3.zero) base.Move();
            else
            {
                var position = Vector3.MoveTowards(_targetData.transform.position, _movePointField.Value,
                    Time.deltaTime * _speed);

                _targetData.transform.position = position;
            }
        }
    }
}