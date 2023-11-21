using Game.Assets;
using UnityEngine;

namespace Units
{
    public interface IUnit : IAsset
    {
        Transform Transform { get; }
        
        void Move(Vector3 direction);
        
        void SetRotation(Quaternion targetRotation);
        
        public void SetScale(Vector3 scale);
    }
}