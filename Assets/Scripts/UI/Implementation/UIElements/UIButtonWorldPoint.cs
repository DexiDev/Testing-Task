using UnityEngine;

namespace Game.UI.UIElements
{
    public class UIButtonWorldPoint : UIButton
    {
        [SerializeField] private Vector3 _offset;
        
        private Camera _camera;
        private Transform _targetTransform;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _targetTransform = null;
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        private void LateUpdate()
        {
            if (_targetTransform == null) return;
            
            _rectTransform.position = _camera.WorldToScreenPoint(_targetTransform.position) + _offset;
        }
    }
}