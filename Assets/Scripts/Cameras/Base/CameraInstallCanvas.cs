using UnityEngine;
using Zenject;

namespace Game.Cameras
{
    public class CameraInstallCanvas : MonoBehaviour
    {
        private Canvas _canvas;
        private Camera _camera;
        
        [Inject]
        private void Install([Inject(Id = "MainCamera")] Camera camera)
        {
            _camera = camera;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            _canvas.worldCamera = _camera;
        }
    }
}