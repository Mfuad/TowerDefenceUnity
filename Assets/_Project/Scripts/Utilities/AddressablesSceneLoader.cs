using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TowerDefense.Scripts.Utilities.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TowerDefense.Assets._Project.Scripts.Utilities
{
    public class AddressablesSceneLoader : ISceneLoader
    {
        public UniTask<SceneInstance> LoadSceneAsync(string sceneName, CancellationToken cancellation = default)
        {
            return Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).WithCancellation(cancellation);
            
        }

        public async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
        {   
            await Addressables.UnloadSceneAsync(sceneInstance);


        }
    }



}