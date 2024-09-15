using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BaseSource
{
    public class SceneLoader : LiveSingleton<SceneLoader>
    {
        [SerializeField] private UILoadingScreen uiLoadingScreen;
        [SerializeField] private float delayTime = 1.5f;

        private SceneInstance _scene;
        private bool _isSceneLoaded;

        private const float MaxLoadingProgress = 0.8f;
        private const float ActiveSceneProgress = 0.2f;

        protected override void OnAwake() { }

        public async UniTask LoadScene(
            string sceneId,
            Func<UniTask> onStartLoadScene = null,
            Func<UniTask> onNewSceneActivated = null,
            Action onLoadingScreenHide = null)
        {
            Debug.Log($"[SceneLoader] Start load scene: {sceneId.Color("yellow")}");
            var loadSceneOperation = Addressables.LoadSceneAsync(sceneId, LoadSceneMode.Single, false);
            loadSceneOperation.Completed += OnLoadSceneCompleted;

            uiLoadingScreen.Show();
            WaitSceneLoaded();

            // chờ loading xong scene && các task khác trong lúc load scene
            await UniTask.WhenAll(
                loadSceneOperation.Task.AsUniTask(),
                onStartLoadScene?.Invoke() ?? UniTask.CompletedTask
            ).ContinueWith(() => _isSceneLoaded = true);

            // bật scene mới lên
            await UniTask.Delay(150);
            await ActiveNewScene();
            await UniTask.Delay(150);

            // chờ loading 100% && init xong scene mới
            await UniTask.WhenAll(
                UpdateActivatingProgress(),
                onNewSceneActivated?.Invoke() ?? UniTask.CompletedTask
            );
            Debug.Log("done active scene & init level".Color("yellow"));

            uiLoadingScreen.Hide();
            onLoadingScreenHide?.Invoke();
        }

        private void OnLoadSceneCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _scene = handle.Result;
            }
        }

        private async void WaitSceneLoaded()
        {
            var currentProgress = 0f;
            var stepProgress = 0.015f;
            while (!_isSceneLoaded)
            {
                currentProgress = Mathf.Min(1f, currentProgress + stepProgress);
                uiLoadingScreen.SetProgress(currentProgress * MaxLoadingProgress);
                await UniTask.Delay(100);
            }
        }

        private async UniTask ActiveNewScene()
        {
            await UniTask.WaitUntil(() => _isSceneLoaded);
            await _scene.ActivateAsync();
            _isSceneLoaded = false;
        }

        private async UniTask UpdateActivatingProgress()
        {
            var time = 0f;
            while (time < delayTime)
            {
                time += Time.fixedDeltaTime;
                uiLoadingScreen.SetProgress(MaxLoadingProgress + (time / delayTime) * ActiveSceneProgress);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }

            uiLoadingScreen.SetProgress(1f);
            await UniTask.Delay(500);
        }
    }
}