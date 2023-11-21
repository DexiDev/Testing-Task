using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIToggle : UIElement
    {
        [SerializeField] protected Toggle _toggle;
        
        public event Action<bool> OnToggleChanged;
        
        protected virtual void OnEnable()
        {
            _toggle.onValueChanged.AddListener(ToggleChanged);
        }

        protected override void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ToggleChanged);
            base.OnDisable();
        }

        protected virtual void ToggleChanged(bool value)
        {
            OnToggleChanged?.Invoke(value);
        }
    }
}