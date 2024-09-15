using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BaseSource
{
    public partial class ScreenManager
    {
        public UIScreen LastScreen => screenStack.Count > 0 ? screenStack.Last() : null;

        public UIScreen Get<T>() where T : UIScreen
        {
            var screen = screenStack.FindAll(p => p.GetType() == typeof(T)).Last();

            if (screen == null)
            {
                Debug.LogError($"[PANEL] Not found panel {typeof(T).Name.Color("red")}");
                return null;
            }

            return screen;
        }

        public async UniTask<T> CreateAndShow<T>(string address) where T : UIScreen
        {
            var panel = await Create<T>(address);
            await panel.Open();
            return panel;
        }

        public async UniTask Hide<T>() where T : UIScreen
        {
            var panel = screenStack.Find(p => p.GetType() == typeof(T));

            if (panel == null)
            {
                Debug.LogWarning($"[PANEL] Not found {typeof(T).Name.Color("red")}");
                return;
            }

            await panel.Close();
        }

        public async UniTask<T> Transition<T>(string address) where T : UIScreen
        {
            // disable interactable of last screen
            var lastScreen = LastScreen;
            lastScreen.SetInteractable(false);

            // setup transition & enable interactable of last screen when new screen is hidden
            var newScreen = await Create<T>(address);
            newScreen.OnPreOpen += () => lastScreen.HideTween().Forget();
            newScreen.OnPreClose += () => lastScreen.ShowTween().Forget();
            newScreen.OnPostClose += () => lastScreen.SetInteractable(true);

            await newScreen.Open();
            return newScreen;
        }
    }
}