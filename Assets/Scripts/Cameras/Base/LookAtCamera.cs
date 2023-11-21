using UnityEngine;
using Zenject;

namespace Game.Cameras
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;
        
        [Inject]
        private void Install([Inject(Id = "MainCamera")] Camera camera)
        {
            _camera = camera;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
    }
}