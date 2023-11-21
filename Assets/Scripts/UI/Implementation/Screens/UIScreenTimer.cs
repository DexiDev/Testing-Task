using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.UI.Implementation.Screens
{
    public class UIScreenTimer : UIScreen
    {
        [SerializeField] private float _duration = 1f;

        private UIManager _uiManager;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Install(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public override void OnShow()
        {
            base.OnShow();
            StartTimer();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _cancellationTokenSource?.Cancel();
        }

        protected void StartTimer()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new();
            Timer(_cancellationTokenSource.Token);
        }
        
        private async void Timer(CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: token);
                _uiManager.HideElement(this);
            }
            catch(OperationCanceledException) {}
        }
    }
}