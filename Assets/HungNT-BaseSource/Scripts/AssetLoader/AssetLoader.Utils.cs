using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace BaseSource
{
    public partial class AssetLoader
    {
        public async UniTask<Sprite> LoadSprite(string atlasAddress, string spriteName)
        {
            try
            {
                var atlas = await Load<SpriteAtlas>(atlasAddress);
                return atlas.GetSprite(spriteName);
            }
            catch (Exception)
            {
                Debug.LogError($"[AssetLoader] Not found sprite: {spriteName.Color("red")} in atlas: {atlasAddress.Color("red")}");
                return null;
            }
        }

        public async UniTask<T> LoadPrefab<T>(string path) where T : MonoBehaviour
        {
            var prefab = await Load<GameObject>(path);
            return prefab.GetComponent<T>();
        }

        public async UniTask<T> Instantiate<T>(string path, Transform parent = null) where T : MonoBehaviour
        {
            var prefab = await LoadPrefab<T>(path);
            return Instantiate(prefab, parent);
        }
    }
}