using Cysharp.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using TowerDefense.Scripts.Utilities.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TowerDefense.Assets._Project.Scripts.Utilities
{
    public class SceneController : ISceneController
    {
        public readonly Dictionary<string, SceneInstance> _loadedScenes = new();
        public ISceneLoader _loader;

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

            foreach (var scene in transition._scenesToUnload)
            {
                await _loader.UnloadSceneAsync(_loadedScenes[scene], cancellation);
            }


            foreach (var kvp in transition._scenesToLoad)
            {
                if (_loadedScenes.ContainsKey(kvp.Key))
                {
                    await _loader.UnloadSceneAsync(_loadedScenes[kvp.Key], cancellation);
                }

                var scene = await kvp.Value;
                scene.ActivateAsync();
                _loadedScenes[kvp.Key] = scene;
            }

        }
    }

    public class SceneTransition
    {
        public readonly Dictionary<string, UniTask<SceneInstance>> _scenesToLoad = new();
        public readonly List<string> _scenesToUnload = new();
        private readonly ISceneController _loader;

        public SceneTransition(ISceneController loader)
        {
            _loader = loader;
        }

        public SceneTransition Load(string slot, string address)
        {
            var handle = Addressables.LoadSceneAsync(address, LoadSceneMode.Additive, activateOnLoad: false)
                .ToUniTask<SceneInstance>();
            _scenesToLoad.Add(slot, handle);
            return this;
        }

        public SceneTransition Unload(string slot)
        {
            _scenesToUnload.Add(slot);
            return this;
        }

        public async UniTask Perform(CancellationToken cancellation = default)
        {
            await _loader.ExecuteTransition(this, cancellation);
        }
    }
    public interface ISceneController
    {
        public SceneTransition Transition();
        public UniTask ExecuteTransition(SceneTransition controller, CancellationToken cancellation = default);

    }

    public class AddressablesSceneLoader : ISceneLoader
    {
        public UniTask<SceneInstance> LoadSceneAsync(string sceneName, CancellationToken cancellation = default)
        {
            return Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).WithCancellation(cancellation);

        }

        public async UniTask UnloadSceneAsync(SceneInstance sceneInstance, CancellationToken cancellation = default)
        {
            await Addressables.UnloadSceneAsync(sceneInstance).WithCancellation(cancellation);
        }
    }
}