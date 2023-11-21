using System;
using Cysharp.Threading.Tasks;
using Game.Assets;
using Game.Loadings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Levels
{
    public class LevelController : SerializedMonoBehaviour
    {
        private LevelManager _levelManager;
        private AssetsManager _assetsManager;
        private LoadingManager _loadingManager;
        
        private LevelData _currentLevelData;

        public LevelData CurrentLevelData => _currentLevelData;
        
        public event Action<bool> OnLevelCompleted;
        
        [Inject]
        private void Install(LevelManager levelManager, AssetsManager assetsManager, LoadingManager loadingManager)
        {
            _levelManager = levelManager;
            _assetsManager = assetsManager;
            _loadingManager = loadingManager;
        }

        private void Awake()
        {
            _currentLevelData = _levelManager.GetData(_levelManager.CurrentLevelID);
        }

        private async void Start()
        {
            await UniTask.WaitWhile(() => _loadingManager.IsLoading);
            InitializeSpawn();
        }

        private void OnEnable()
        {
            _levelManager.RegisterController(this);
        }

        private void OnDisable()
        {
            _levelManager.UnregisterController(this);
        }
        
        private void InitializeSpawn()
        {
            
        }

        public void FailedLevel()
        {
            _levelManager.CompletedLevel(false);
            OnLevelCompleted?.Invoke(false);
        }
        
        public void CompletedLevel()
        {
            _levelManager.CompletedLevel(true);
            OnLevelCompleted?.Invoke(true);
        }
    }
}