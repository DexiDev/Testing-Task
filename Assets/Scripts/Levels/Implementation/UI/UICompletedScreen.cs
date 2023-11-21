using Game.UI;
using UnityEngine;

namespace Game.Levels.UI
{
    public class UICompletedScreen : UIScreen
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        
        private LevelData _levelData;

        public void Initialize(LevelData levelData, bool isWin)
        {
            _levelData = levelData;
            
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);
            
            if(isWin) _winPanel.SetActive(true);
            else _losePanel.SetActive(true);
        }
    }
}