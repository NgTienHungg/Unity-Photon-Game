using DG.Tweening;
using UnityEngine;

namespace BaseSource
{
    [CreateAssetMenu(menuName = "Base/TweenSettings")]
    public class TweenSettings : ScriptableObject
    {
        [Space]
        public Ease easeIn = Ease.Linear;
        public Ease easeOut = Ease.Linear;

        [Space]
        public float durationIn = 0.25f;
        public float durationOut = 0.25f;
    }
}