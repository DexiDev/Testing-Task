using Game.UI;
using Zenject;

namespace Game.Levels.UI
{
    public class UILoadLevelButton : UIButton
    {
        private LevelManager _levelManager;
        
        [Inject]
        private void Install(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        protected override void Click()
        {
            base.Click();
            
            _levelManager.LoadCurrentLevel();
        }
    }
}