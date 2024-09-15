using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace BaseSource
{
    public partial class AssetLoader : LiveSingleton<AssetLoader>, IAssetLoader
    {
        [ShowInInspector]
        private Dictionary<Type, Dictionary<string, object>> assetsCached;

        private List<AsyncOperationHandle> listRequest;

        protected override void OnAwake()
        {
            assetsCached = new Dictionary<Type, Dictionary<string, object>>();
            listRequest = new List<AsyncOperationHandle>();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        public async UniTask<TAsset> Load<TAsset>(string address) where TAsset : Object
        {
            var type = typeof(TAsset);
            if (!assetsCached.TryGetValue(type, out var assetsOfType))
            {
                Debug.Log("[AssetLoader] Add new type of asset: " + type.Name.Color("yellow"));
                assetsOfType = new Dictionary<string, object>();
                assetsCached[type] = assetsOfType;
            }

            if (assetsOfType.TryGetValue(address, out var asset))
            {
                return (TAsset)asset;
            }

            var requestHandle = Addressables.LoadAssetAsync<TAsset>(address);
            listRequest.Add(requestHandle);

            await requestHandle.Task;
            assetsOfType[address] = requestHandle.Result;
            return requestHandle.Result;
        }

        public void ReleaseAll()
        {
            foreach (var request in listRequest)
                Addressables.ReleaseInstance(request);

            listRequest.Clear();
            assetsCached.Clear();
            Debug.Log("[AssetLoader] Released all assets".Color("red"));
        }

        private void OnActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
        {
            ReleaseAll();
        }
    }
}