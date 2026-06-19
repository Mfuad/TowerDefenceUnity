using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Utilities
{
    public class AddressableProvider
    {
        private Dictionary<string, AsyncOperationHandle> _cache = new();

        public async UniTask<T> LoadAssetAsync<T>(
            string address,
            CancellationToken cancellation = default)
            where T : ScriptableObject
        {
            return await GetFromCacheOrLoadAsync<T>(
                address,
                () => Addressables.LoadAssetAsync<T>(address),
                cancellation);
        }

        public async UniTask<SceneInstance> LoadSceneAsync(
            string address,
            LoadSceneMode mode = LoadSceneMode.Single,
            CancellationToken cancellation = default)
        {
            return await GetFromCacheOrLoadAsync<SceneInstance>(
                address,
                () => Addressables.LoadSceneAsync(address, mode),
                cancellation);
        }

        public async UniTask Release(string address)
        {
            await ReleaseAndRemoveFromCacheAsync(address);
        }


        private async UniTask<T> GetFromCacheOrLoadAsync<T>(
            string address,
            Func<AsyncOperationHandle<T>> callback,
            CancellationToken cancellation = default)
        {
            (bool found, T result) = await TryResolveFromCache<T>(address, cancellation);
            if (found) return result;

            return await LoadFromAddressables<T>(address, callback, cancellation);
        }

        private async UniTask<T> LoadFromAddressables<T>(
            string address, 
            Func<AsyncOperationHandle<T>> callback,
            CancellationToken cancellation = default)
        {
            var loadHandle = callback();
            _cache[address] = loadHandle;

            try
            {
                return await loadHandle.WithCancellation(cancellation);
            }
            catch
            {
                await ReleaseAndRemoveFromCacheAsync(address);

                throw new Exception($"Failed to load '{address}'");
            }
        }

        private async UniTask<T> ResolveFromCacheAsync<T>(
            string address,
            CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException($"Address cannot be null or empty.", nameof(address));

            if (!_cache.TryGetValue(address, out AsyncOperationHandle handle) || !handle.IsValid())
                throw new KeyNotFoundException($"No valid cache entry found for address: '{address}'.");

            if (!handle.IsDone)
            {
                await handle.WithCancellation(cancellation);
                cancellation.ThrowIfCancellationRequested();
            }

            if (handle.Result is T cached)
                return cached;

            throw new InvalidOperationException(
                $"Type mismatch at '{address}': expected '{typeof(T).Name}', " +
                $"got '{handle.Result?.GetType().Name ?? "null"}'."
            );
        }

        private async UniTask<(bool success, T result)> TryResolveFromCache<T>(
            string address,
            CancellationToken cancellation = default)
        {
            try
            {
                cancellation.ThrowIfCancellationRequested();
                T value = await ResolveFromCacheAsync<T>(address, cancellation);
                return (true, value);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch
            {
                return (false, default);
            }
        }

        private async UniTask ReleaseAndRemoveFromCacheAsync(
            string address,
            CancellationToken cancellation = default)
        {
            if (_cache.TryGetValue(address, out var value))
            {

                if (value.IsValid())
                {
                    if (value.Result is SceneInstance scene)
                        await Addressables.UnloadSceneAsync(scene).WithCancellation(cancellation);
                    else
                        Addressables.Release(value);
                }

                _cache.Remove(address);
            }
        }

    }
}

        //public async UniTask<T> LoadAssetAsync<T>(
        //    AddresableGroup group,
        //    CancellationToken cancellation = default)
        //    where T : ScriptableObject
        //{
        //    return await LoadAssetAsync<T>(group, typeof(T).Name, cancellation);
        //}

        //public async UniTask<T> LoadAssetAsync<T>(
        //    AddresableGroup group,
        //    string name,
        //    CancellationToken cancellation = default)
        //    where T : ScriptableObject
        //{
        //    string address = Address(group, name);
        //    return await LoadAssetAsync<T>(address, cancellation);
        //}