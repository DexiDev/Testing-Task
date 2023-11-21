using System;
using System.Linq;
using Game.Assets;
using Game.Data;
using Game.Data.Implementation.Fields;
using Game.InteractiveObjects;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Units.Shotings
{
    public class BulletController : UnitController, IAsset
    {
        [SerializeField, TabGroup("Components")] private SphereCollider _collider;
        [SerializeField, TabGroup("Parameters")] private float _maxDistance;
        [SerializeField, TabGroup("Parameters")] private LayerMask _layerMaskTarget;


        private Vector3 _startPosition;
        private Vector3 _defaultScale;
        private DamageRadiusField _damageRadiusField;

        public Vector3 DefaultScale => _defaultScale;
        public GameObject Asset => gameObject;
        
        
        public event Action<IAsset> OnReleased;

        protected override void Awake()
        {
            base.Awake();
            _defaultScale = transform.localScale;
            _damageRadiusField = GetDataField<DamageRadiusField>(true);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _startPosition = transform.position;
            _isMoveField.SetValue(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            var hitPoint = collider.ClosestPoint(transform.position);
            
            var raycastHits = GetCastHit(hitPoint, 0f, _damageRadiusField.Value);
            
            foreach (var hit in raycastHits)
            {
                if (hit.transform.TryGetComponent(out ExploreObject exploreObject))
                {
                    exploreObject.gameObject.SetActive(false);
                }
            }
            
            gameObject.SetActive(false);
        }

        public void Shoot()
        {
            _isMoveField.SetValue(true);
        }

        public override void Move(Vector3 direction)
        {
            base.Move(direction);
            
            if(Vector3.Distance(transform.position, _startPosition) >= _maxDistance) 
                gameObject.SetActive(false);
        }

        public RaycastHit[] GetCastHitForward(Vector3 position, float radiusFactor = 1f)
        {
            return GetCastHit(position, _maxDistance, radiusFactor);
        }
        
        public RaycastHit[] GetCastHit(Vector3 position, float maxDistance, float radiusFactor = 1f)
        {
            return Physics.SphereCastAll(position, _collider.radius * transform.lossyScale.magnitude * radiusFactor, transform.forward, maxDistance, _layerMaskTarget);
        }
    }
}