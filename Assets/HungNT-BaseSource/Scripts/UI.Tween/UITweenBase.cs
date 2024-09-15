using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaseSource
{
    public abstract class UITweenBase : MonoBehaviour, ITween
    {
        #region ===== Fields =====
        [Title("Base")]
        [SerializeField] protected TweenSettings settings;

        [SerializeField] protected bool overrideEase;
        [SerializeField] [ShowIf("@overrideEase")] protected Ease easeIn = Ease.Linear;
        [SerializeField] [ShowIf("@overrideEase")] protected Ease easeOut = Ease.Linear;

        [SerializeField] protected bool overrideDuration;
        [SerializeField] [ShowIf("@overrideDuration")] protected float durationIn = 0.25f;
        [SerializeField] [ShowIf("@overrideDuration")] protected float durationOut = 0.25f;

        [SerializeField] protected bool delay;
        [SerializeField] [ShowIf("@delay")] protected float delayIn;
        [SerializeField] [ShowIf("@delay")] protected float delayOut;
        #endregion

        #region ===== Properties =====
        protected Ease EaseIn => overrideEase ? easeIn : settings.easeIn;
        protected Ease EaseOut => overrideEase ? easeOut : settings.easeOut;
        protected float DurationIn => overrideDuration ? durationIn : settings.durationIn;
        protected float DurationOut => overrideDuration ? durationOut : settings.durationOut;
        protected float DelayIn => delay ? delayIn : 0f;
        protected float DelayOut => delay ? delayOut : 0f;
        #endregion

        protected virtual string SettingsPath => "TweenSettings";

        protected CancellationTokenSource showCts;

        protected virtual void Reset()
        {
            settings = Resources.Load<TweenSettings>(SettingsPath);
        }

        // private void OnEnable()
        // {
        //     Init();
        //     Show().Forget();
        // }

        public virtual void Init()
        {
            Setup();
            Inactive();
        }

        protected virtual void Setup()
        {
            if (settings == null)
            {
                settings = Resources.Load<TweenSettings>(SettingsPath);
            }
        }

        public virtual UniTask Show()
        {
            showCts?.Cancel();
            showCts = new CancellationTokenSource();
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            showCts?.Cancel();
            showCts = new CancellationTokenSource();
            return UniTask.CompletedTask;
        }

        protected abstract void Active();

        protected abstract void Inactive();

        private void OnDisable()
        {
            showCts?.Cancel();
            showCts?.Dispose();
            showCts = null;
        }
    }
}