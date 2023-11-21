using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIButton : UIElement
    {
        [SerializeField] private Button _button;
        
        public event Action OnClick;
        
        protected virtual void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        protected override void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
            base.OnDisable();
        }
        
        protected virtual void Click()
        {
            OnClick?.Invoke();
        }
    }
}