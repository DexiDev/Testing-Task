using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Game.Data
{
    public abstract class DataController : SerializedMonoBehaviour, IDataController
    {
        [OdinSerialize] private List<IDataField> _dataFields;

        public event Action<IDataField> OnDataAdded;
        public event Action<IDataField> OnDataRemoved;
        
        public bool HasData<T>() where T : class, IDataField, new()
        {
            return GetDataField<T>() != null;
        }
        
        public T[] GetDataFields<T>() where T : class, IDataField
        {
            return (T[])_dataFields.Where(field => field is T).ToArray();
        }
        
        public T GetDataField<T>(T dataField = null) where T : class, IDataField
        {
            Type targetType = ReferenceEquals(null, dataField) ? typeof(T) : dataField.GetType();
            T targetDataField = _dataFields.FirstOrDefault(field => field.GetType() == targetType) as T;
            
            return targetDataField;
        }
        
        public T GetDataField<T>(bool isAutoCreate, T dataField = null) where T : class, IDataField, new()
        {
            T targetDataField = GetDataField<T>(dataField);
            
            if (isAutoCreate && ReferenceEquals(null, targetDataField))
            {
                targetDataField = new T();
                AddDataField(targetDataField);
            }

            return targetDataField;
        }

        public bool TryGetDataField<T>(out T dataField) where T : class, IDataField, new()
        {
            dataField = GetDataField<T>();
            
            return dataField != null;
        }


        public bool TryAddDataField<T>(T dataField = null) where T : class, IDataField, new()
        {
            if (HasData<T>()) return false;
            
            AddDataField<T>(dataField);
            return true;
        }
        
        public void AddDataField<T>(T dataField) where T : class, IDataField, new()
        {
            if (ReferenceEquals(null, dataField)) dataField = new T();
            
            _dataFields.Add(dataField);
            OnDataAdded?.Invoke(dataField);
        }
        
        public void RemoveDataField<T>(T dataField) where T : class, IDataField
        {
            _dataFields.Remove(dataField);
            OnDataRemoved?.Invoke(dataField);
        }
    }
}