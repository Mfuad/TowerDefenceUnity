using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Utilities
{
    public class AddressablesSceneLoader
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