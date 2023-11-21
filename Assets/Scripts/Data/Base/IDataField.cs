using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data
{
    public interface IDataField
    {
        void SetInstance(IDataField dataField);
    }
    
    [Serializable]
    public class IDataField<T> : IDataField
    {
        [SerializeField, OnValueChanged(nameof(OnValueChanged))] protected T _value;

        public virtual T Value
        {
            get => _value;
            protected set => _value = value;
        }

        public event Action<T> OnChanged;
        public event Action<IDataField<T>> OnDataChanged;
        
        public virtual void SetValue(T value)
        {
            if (!Equals(Value, value))
            {
                Value = value;
                OnChanged?.Invoke(value);
                InvokeDataChanged();
            }
        }

        protected virtual void OnValueChanged(T value)
        {
            OnChanged?.Invoke(value);
            InvokeDataChanged();
        }

        protected void InvokeDataChanged()
        {
            OnDataChanged?.Invoke(this);
        }

        protected virtual bool Equals(T oldValue, T newValue)
        {
            return oldValue == null && newValue == null || oldValue != null && oldValue.Equals(newValue);
        }
        
        public virtual void SetInstance(IDataField dataField)
        {
            if (dataField is IDataField<T> data)
            {
                if (Value == null && data.Value != null || !Value.Equals(data.Value))
                {
                    SetValue(data.Value);
                }
            }
        }
    }
}