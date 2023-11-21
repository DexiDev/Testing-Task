namespace Game.UI.UIElements
{
    #if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
    public class UIRewardButton : UIButton
    {
        protected override void Click()
        {
            //if Reward
            base.Click();
        }
    }
    #endif
}