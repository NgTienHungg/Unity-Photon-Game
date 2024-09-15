using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BaseSource
{
    public interface IAssetLoader
    {
        UniTask<TAsset> Load<TAsset>(string address) where TAsset : Object;

        void ReleaseAll();
    }
}