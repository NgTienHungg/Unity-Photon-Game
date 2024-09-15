using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BaseSource
{
    public partial class ScreenManager : MonoSingleton<ScreenManager>
    {
        [ShowInInspector]
        private List<UIScreen> screenStack = new List<UIScreen>();

        protected override void OnAwake()
        {
            screenStack = GetComponentsInChildren<UIScreen>().ToList();
        }

        private async void Start()
        {
            await UniTask.WhenAll(Enumerable.Select(screenStack, screen => screen.Init()).ToList());
            screenStack.ForEach(screen => screen.Open().Forget());
        }

        public async UniTask<TScreen> Create<TScreen>(string address) where TScreen : UIScreen
        {
            var screen = (await Addressables.InstantiateAsync(address, transform)).GetComponent<TScreen>();
            await screen.Init();
            screen.OnPreOpen += () => PushBackScreen(screen);
            screen.OnPostClose += () => PopBackScreen(screen);
            return screen;
        }

        private void PushBackScreen(UIScreen uiScreen)
        {
            screenStack.Add(uiScreen);
        }

        private void PopBackScreen(UIScreen uiScreen)
        {
            screenStack.Remove(uiScreen);
        }

#if UNITY_EDITOR || UNITY_ANDROID
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TryCloseCurrentScreen();
            }
        }

        private void TryCloseCurrentScreen()
        {
            if (screenStack.Count == 0 || !screenStack.Last().CanBack)
            {
                Debug.LogWarning("[Screen] Can't back");
                return;
            }

            screenStack.Last().Close().Forget();
        }
#endif
    }
}