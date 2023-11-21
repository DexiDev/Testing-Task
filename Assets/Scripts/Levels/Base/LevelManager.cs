using System;
using Game.Data;
using Game.Loadings;
using Game.UI;
using Zenject;

namespace Game.Levels
{
    public class LevelManager : DataManager<LevelData, LevelConfig>
    {
        private int _currentLevelLoop;
        private int _currentLevelIndex;
        private LevelController _levelController;

        private UIManager _uiManager;
        private LoadingManager _loadingManager;

        public int CurrentLevelLoop => _currentLevelLoop;
        public string CurrentLevelID => _config.Datas[_currentLevelIndex]?.ID;
        public LevelController LevelController => _levelController;

        public event Action<int> OnLevelChanged;
        public event Action<int> OnLevelLoopChanged;
        
        [Inject]
        private void Install(UIManager uiManager, LoadingManager loadingManager)
        {
            _uiManager = uiManager;
            _loadingManager = loadingManager;
        }

        protected override void Initialized()
        {
            base.Initialized();
        }
        
        public void RegisterController(LevelController levelController)
        {
            _levelController = levelController;
        }

        public void UnregisterController(LevelController levelController)
        {
            if (_levelController == levelController)
                _levelController = null;
        }

        public void IncreaseLevel()
        {
            _currentLevelLoop++;
            _currentLevelIndex++;

            if (_currentLevelIndex >= _datas.Count) _currentLevelIndex = _config.SkipForLoop;
            
            OnLevelChanged?.Invoke(_currentLevelIndex);
            OnLevelLoopChanged?.Invoke(_currentLevelLoop);
        }
        
        public void LoadCurrentLevel()
        {
            var levelData = _config.Datas[_currentLevelIndex];

            if (levelData != null)
            {
                _loadingManager.Load(levelData.LoadingID);   
            }
        }

        public void CompletedLevel(bool isWin)
        {
            var uiCompletedScreen = _uiManager.ShowElement(_config.UICompletedScreen);
            uiCompletedScreen.Initialize(GetData(CurrentLevelID), isWin);
            
            if(isWin) IncreaseLevel();
        }
    }
}