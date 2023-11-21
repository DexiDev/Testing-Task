using System;
using UnityEngine;

namespace Game.Assets.Assets
{
    [DisallowMultipleComponent]
    public class AssetResource : MonoBehaviour, IAsset
    {
        public GameObject Asset => gameObject;
        public event Action<IAsset> OnReleased;

        private void OnDisable()
        {
            OnReleased?.Invoke(this);
        }
    }
}