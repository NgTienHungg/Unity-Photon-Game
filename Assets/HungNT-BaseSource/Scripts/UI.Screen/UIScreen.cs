using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BaseSource
{
    [RequireComponent(typeof(TweenPlayer), typeof(CanvasGroup))]
    public abstract class UIScreen : MonoBehaviour, IScreen
    {
        [SerializeField] protected TweenPlayer tweenPlayer;
        [SerializeField] protected CanvasGroup canvasGroup;

        public abstract bool CanBack { get; }
        public Action OnPreOpen { get; set; }
        public Action OnPostOpen { get; set; }
        public Action OnPreClose { get; set; }
        public Action OnPostClose { get; set; }

        private CancellationTokenSource _tweenCTS;

        protected virtual void Reset()
        {
            tweenPlayer = GetComponent<TweenPlayer>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual async UniTask Init()
        {
            gameObject.SetActive(false);
            tweenPlayer.Init();
            await UniTask.CompletedTask;
        }

        public async UniTask Open()
        {
            _tweenCTS?.Cancel();
            _tweenCTS = new CancellationTokenSource();

            OnPreOpen?.Invoke();
            gameObject.SetActive(true);
            await ShowTween();
            OnPostOpen?.Invoke();
        }

        public async UniTask Close()
        {
            _tweenCTS?.Cancel();
            _tweenCTS = new CancellationTokenSource();

            OnPreClose?.Invoke();
            await HideTween();
            Destroy(gameObject);
            OnPostClose?.Invoke();
        }

        public UniTask ShowTween()
        {
            return tweenPlayer.ShowTween(_tweenCTS.Token);
        }

        public UniTask HideTween()
        {
            return tweenPlayer.HideTween(_tweenCTS.Token);
        }

        public void SetInteractable(bool interactable)
        {
            canvasGroup.interactable = interactable;
        }
    }
}