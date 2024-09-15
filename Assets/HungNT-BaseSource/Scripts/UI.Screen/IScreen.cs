using System;
using Cysharp.Threading.Tasks;

namespace BaseSource
{
    public interface IScreen
    {
        Action OnPreOpen { get; set; }
        Action OnPostOpen { get; set; }
        Action OnPreClose { get; set; }
        Action OnPostClose { get; set; }

        bool CanBack { get; }
        void SetInteractable(bool interactable);

        UniTask Init();
        UniTask Open();
        UniTask Close();
        UniTask ShowTween();
        UniTask HideTween();
    }
}