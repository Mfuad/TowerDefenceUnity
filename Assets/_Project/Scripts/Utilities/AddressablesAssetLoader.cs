using Cysharp.Threading.Tasks;
using System.Threading;
using TowerDefense.Scripts.Utilities.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TowerDefense.Scripts.Utilities
{
    public class AddressablesAssetLoader : IAssetLoader
    {
        private readonly RefCountDictionary<string, AsyncOperationHandle> _storage = new();

        public UniTask<T> GetAsync<T>(string address = default, CancellationToken cancellation = default)
        {
            address ??= $"Config/{typeof(T).Name}.asset";

            if(!_storage.TryGet(address,out var handle))
            {
                handle = Addressables.LoadAssetAsync<T>(address);
                _storage.Add(address, handle);
            }

            return handle.Convert<T>().WithCancellation(cancellation);
        }

        public void Release<T>(string address = default)
        {
            address ??= $"/Config/{typeof(T).Name}.asset";
            
            if( _storage.TryRelease(address,out var handle))
            {
                Addressables.Release(handle);
            }
        }

    }
}