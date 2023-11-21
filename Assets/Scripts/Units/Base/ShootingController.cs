using Game.Assets;
using Units.Shotings;
using UnityEngine;
using Zenject;

namespace Units
{
    [DisallowMultipleComponent]
    public class ShootingController : UnitController
    {
        [SerializeField] private LineRenderer _shootingAim;
        [SerializeField] private BulletController _bulletAsset;
        [SerializeField] private Transform _bulletSpawnPoint;
        
        private AssetsManager _assetsManager;
        
        protected BulletController _bullet;

        public Transform ShootingAim => _shootingAim.transform;
        
        [Inject]
        private void Install(AssetsManager assetsManager)
        {
            _assetsManager = assetsManager;
        }

        public void SetAimRotation(Quaternion rotation)
        {
            _shootingAim.transform.localRotation = rotation;
        }

        public void SetAimActive(bool isActive)
        {
            _shootingAim.gameObject.SetActive(isActive);
        }

        public BulletController GetBullet()
        {
            if (_bullet != null) return null;
            
            _bullet = _assetsManager.GetAsset<BulletController>(_bulletAsset, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletSpawnPoint);

            return _bullet;
        }

        public void Shoot()
        {
            if (_bullet == null)
            {
                _bullet = GetBullet();
            }

            _bullet.transform.SetParent(null, true);
            
            _bullet.transform.forward = _shootingAim.transform.forward;
            _bullet.Shoot();

            _bullet = null;
        }

        public override void SetScale(Vector3 scale)
        {
            base.SetScale(scale);
            _shootingAim.widthMultiplier = scale.y;
        }
    }
}