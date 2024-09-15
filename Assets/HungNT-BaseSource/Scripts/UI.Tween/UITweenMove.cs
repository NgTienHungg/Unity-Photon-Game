using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaseSource
{
    public class UITweenMove : UITweenBase
    {
        [Title("Move")]
        [SerializeField] private RectTransform rectTrans;
        [SerializeField] private Vector2 offset;

        private Vector2 activePos;
        private Vector2 inactivePos;

        protected override string SettingsPath => "TweenSettings_Move";

        protected override void Reset()
        {
            base.Reset();
            rectTrans = transform as RectTransform;
        }

        protected override void Setup()
        {
            base.Setup();

            if (rectTrans == null)
            {
                rectTrans = transform as RectTransform;
            }

            activePos = rectTrans.anchoredPosition;
            inactivePos = activePos + offset;
        }

        public override async UniTask Show()
        {
            await base.Show();
            await rectTrans.DOAnchorPos(activePos, DurationIn)
                .SetEase(EaseIn).SetDelay(DelayIn)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Active);
        }

        public override async UniTask Hide()
        {
            await base.Hide();
            await rectTrans.DOAnchorPos(inactivePos, DurationOut)
                .SetEase(EaseOut).SetDelay(DelayOut)
                .ToUniTask(cancellationToken: showCts.Token).ContinueWith(Inactive);
        }

        protected override void Active()
        {
            rectTrans.anchoredPosition = activePos;
        }

        protected override void Inactive()
        {
            rectTrans.anchoredPosition = inactivePos;
        }
    }
}