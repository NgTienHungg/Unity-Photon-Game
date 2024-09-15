using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BaseSource
{
    public class UITweenScale : UITweenBase
    {
        [Header("Scale")]
        [SerializeField] private float inactiveScale;
        [SerializeField] private float activeScale = 1f;

        protected override string SettingsPath => "TweenSettings_Scale";

        public override async UniTask Show()
        {
            await base.Show();
            await transform.DOScale(activeScale, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Active);
        }

        public override async UniTask Hide()
        {
            await base.Hide();
            await transform.DOScale(inactiveScale, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Inactive);
        }

        protected override void Active()
        {
            transform.localScale = Vector3.one * activeScale;
        }

        protected override void Inactive()
        {
            transform.localScale = Vector3.one * inactiveScale;
        }
    }
}