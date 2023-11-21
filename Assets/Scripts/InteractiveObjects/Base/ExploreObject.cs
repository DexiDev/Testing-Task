using System;
using UnityEngine;

namespace Game.InteractiveObjects
{
    public class ExploreObject : MonoBehaviour
    {
        [SerializeField] private Color _selectedColor;
        [SerializeField] private MeshRenderer _meshRenderer;

        private bool _isSelected;
        private Color _defaultColor;

        private void Awake()
        {
            _defaultColor = _meshRenderer.material.color;
        }

        public void SetSelected(bool isSelected)
        {
            if (_isSelected.Equals(isSelected)) return;

            _isSelected = isSelected;

            var material = _meshRenderer.material;

            material.color = isSelected ? _selectedColor : _defaultColor;

            _meshRenderer.material = material;
        }
    }
}