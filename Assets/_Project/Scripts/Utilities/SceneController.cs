using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using TowerDefense.Scripts.Utilities;
using TowerDefense.Scripts.Utilities.Core;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Utilities
{
    public class SceneController : ISceneController
    {
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new();
        private readonly ISceneLoader _loader;
        private readonly SemaphoreSlim _semaphore = new(1,1);

        public SceneController(ISceneLoader loader) 
        {
            _loader = loader;
        }

        public SceneTransition Transition()
        {
            return new SceneTransition(this);
        }

        public async UniTask ExecuteTransition(SceneTransition transition, CancellationToken cancellation = default)
        {
            await _semaphore.WaitAsync(cancellation).AsUniTask();

            try
            {
                //using (var overlay = new ShowLoadingScreenDisposable())
                //{
                //    overlay.SetPercentage(0.5f);
                //}
                await UnloadScenes(transition, cancellation);
                await LoadScenes(transition, cancellation);
            }
            catch 
            {
                _semaphore.Release();
            }
        }

        private async UniTask UnloadScenes(SceneTransition transition,CancellationToken cancellation = default)
        {
            foreach (var scene in transition.ScenesToUnload)
            {
                await _loader.UnloadSceneAsync(_loadedScenes[scene], cancellation);
            }
        }

        private async UniTask LoadScenes(SceneTransition transition, CancellationToken cancellation = default)
        {
            foreach (var kvp in transition.ScenesToLoad)
            {
                if (_loadedScenes.TryGetValue(kvp.Key, out var sceneInstance))
                {
                    await _loader.UnloadSceneAsync(sceneInstance, cancellation);
                }

                var scene = await kvp.Value;
                scene.ActivateAsync();
                _loadedScenes[kvp.Key] = scene;
            }
        }
    }

    public class SceneTransition
    {
        public readonly Dictionary<string, UniTask<SceneInstance>> ScenesToLoad = new();
        public readonly List<string> ScenesToUnload = new();
        private readonly ISceneController _loader;

        public SceneTransition(ISceneController loader)
        {
            _loader = loader;
        }

        public SceneTransition Load(string slot, string address)
        {
            var handle = Addressables.LoadSceneAsync(address, LoadSceneMode.Additive, activateOnLoad: false)
                .ToUniTask<SceneInstance>();
            ScenesToLoad.Add(slot, handle);
            return this;
        }

        public SceneTransition Unload(string slot)
        {
            ScenesToUnload.Add(slot);
            return this;
        }

        public async UniTask Perform(CancellationToken cancellation = default)
        {
            await _loader.ExecuteTransition(this, cancellation);
        }
    }


    public class ShowLoadingScreenDisposable : IDisposable
    {
        private readonly LoadingScreen _loadingScreen;

        private ShowLoadingScreenDisposable(LoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
            _loadingScreen.Show();
        }

        public void SetPercentage(float normalized)
        {
            _loadingScreen.SetPercentage(normalized);
        }

        public void Dispose()
        {
            _loadingScreen.Hide();
        }
    }
}