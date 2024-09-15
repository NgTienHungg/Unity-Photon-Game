using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseSource
{
    public class UIButtonScalable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float originScale = 1f;
        [SerializeField] private float pressScaleMultiplier = 0.9f;
        [SerializeField] private float releaseScaleMultiplier = 1.1f;

        private void Reset()
        {
            originScale = transform.localScale.x;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnHoldEvent();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnReleaseEvent();
        }

        private void OnHoldEvent()
        {
            transform.DOScale(originScale * pressScaleMultiplier, 0.1f)
                .SetUpdate(true);
        }

        private void OnReleaseEvent()
        {
            var sequence = DOTween.Sequence().SetUpdate(true);
            sequence.Append(transform.DOScale(originScale * releaseScaleMultiplier, 0.1f))
                .Append(transform.DOScale(originScale, 0.1f));
        }

        private void OnDisable()
        {
            transform.DOKill();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}