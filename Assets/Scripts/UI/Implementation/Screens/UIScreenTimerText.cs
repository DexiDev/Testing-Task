using TMPro;
using UnityEngine;

namespace Game.UI.Implementation.Screens
{
    public class UIScreenTimerText : UIScreenTimer
    {
        [SerializeField] private TMP_Text _labelTextField;

        public void Initialize(string label)
        {
            _labelTextField.text = label;
            StartTimer();
        }
    }
}