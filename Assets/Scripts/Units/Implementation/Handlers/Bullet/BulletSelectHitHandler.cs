using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Data.Implementation.Fields;
using Game.InteractiveObjects;
using Sirenix.Utilities;
using Units.Shotings;

namespace Units.Handlers.Bullet
{
    public class BulletSelectHitHandler : IHandler<BulletController>
    {
        private IsMoveField _isMoveField;
        private DamageRadiusField _damageRadiusField;
        private CancellationTokenSource _cancellationToken;
        
        private void Awake()
        {
            _isMoveField = _targetData.GetDataField<IsMoveField>();
            _damageRadiusField = _targetData.GetDataField<DamageRadiusField>();
        }

        private void OnEnable()
        {
            OnMoveChanged(_isMoveField.Value);
            _isMoveField.OnChanged += OnMoveChanged;
        }

        private void OnDisable()
        {
            _isMoveField.OnChanged -= OnMoveChanged;
            _cancellationToken?.Cancel();
        }
        
        private void OnMoveChanged(bool isMove)
        {
            _cancellationToken?.Cancel();
            if (!isMove)
            {
                _cancellationToken = new();
                UpdateSelected(_cancellationToken.Token);
            } 
        }

        private async void UpdateSelected(CancellationToken token)
        {
            List<ExploreObject> exploreObjectPool = new();
            
            while (!token.IsCancellationRequested)
            {
                exploreObjectPool.ForEach(exploreObject => exploreObject.SetSelected(false));
                exploreObjectPool.Clear();
                
                var raycastHits = _targetData.GetCastHitForward(_targetData.transform.position);

                if (raycastHits != null)
                {
                    raycastHits.Sort((x, y) => x.distance.CompareTo(y.distance));

                    var hit = raycastHits.FirstOrDefault();
                    
                    var pointHits = _targetData.GetCastHit(hit.point, 0f, _damageRadiusField.Value);

                    foreach (var pointHit in pointHits)
                    {
                        if (pointHit.transform.TryGetComponent(out ExploreObject exploreObject))
                        {
                            exploreObject.SetSelected(true);
                            exploreObjectPool.Add(exploreObject);
                        }
                    }
                }
                await UniTask.Yield();
            }
        }
    }
}