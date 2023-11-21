using UnityEngine;
using Zenject;

namespace Game.Levels.Installers
{
    public class LevelManagerInstaller : MonoInstaller<LevelManagerInstaller>
    {
        [SerializeField] private LevelConfig _config;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelConfig>().FromInstance(_config).AsSingle();
        }
    }
}