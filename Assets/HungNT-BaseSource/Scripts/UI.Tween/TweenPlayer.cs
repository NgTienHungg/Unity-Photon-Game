using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaseSource
{
    public class TweenPlayer : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly]
        private List<ITween> uiTweens = new List<ITween>();

        public void Init()
        {
            uiTweens = GetComponentsInChildren<ITween>(includeInactive: true).ToList();
            uiTweens.ForEach(tween => tween.Init());
        }

        public UniTask ShowTween(CancellationToken token)
        {
            var showTasks = uiTweens.Select(tween => tween.Show()).ToList();
            return UniTask.WhenAll(showTasks).AttachExternalCancellation(token);
        }

        public UniTask HideTween(CancellationToken token)
        {
            var hideTasks = uiTweens.Select(tween => tween.Hide()).ToList();
            return UniTask.WhenAll(hideTasks).AttachExternalCancellation(token);
        }
    }
}