using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIElements
{
    public class UIToggleMark : UIElement
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _speedToggle;
        
        private RectTransform _rectTransform;
        private Vector2 _targetPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleChanged);
            
            OnToggleChanged(_toggle.isOn);

            _rectTransform.anchoredPosition = _targetPosition;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
        
        public void OnToggleChanged(bool value)
        {
            _targetPosition = new Vector2(value ? _offsetX : -_offsetX, _rectTransform.anchoredPosition.y);
        }

        private void Update()
        {
            if (_rectTransform.anchoredPosition != _targetPosition)
            {
                _rectTransform.anchoredPosition = Vector2.MoveTowards(_rectTransform.anchoredPosition, _targetPosition,
                    _speedToggle * Time.deltaTime);
            }
        }
    }
}