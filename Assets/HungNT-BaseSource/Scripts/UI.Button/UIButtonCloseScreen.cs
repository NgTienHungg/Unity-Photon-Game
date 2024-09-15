using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BaseSource
{
    public class UIButtonCloseScreen : UIButtonBase
    {
        [SerializeField] private UIScreen screen;

        protected override void Reset()
        {
            base.Reset();
            screen = GetComponentInParent<UIScreen>();
        }

        protected override void OnClick()
        {
            screen.Close().Forget();
        }
    }
}