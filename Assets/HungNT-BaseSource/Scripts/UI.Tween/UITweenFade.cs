using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaseSource
{
    public class UITweenFade : UITweenBase
    {
        [Title("Fade")]
        [SerializeField] private CanvasGroup canvasGroup;

        protected override string SettingsPath => "TweenSettings_Fade";

        protected override void Reset()
        {
            base.Reset();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void Setup()
        {
            base.Setup();

            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();

                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
        }

        public override async UniTask Show()
        {
            await base.Show();
            await canvasGroup.DOFade(1, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Active);
        }

        public override async UniTask Hide()
        {
            canvasGroup.interactable = false;
            await base.Hide();
            await canvasGroup.DOFade(0, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Inactive);
        }

        protected override void Active()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        protected override void Inactive()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }
}