using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace TowerDefense.Scripts.Utilities.Core
{
    public interface ISceneLoader
    {
        UniTask<SceneInstance> LoadSceneAsync(
            string address = default,
            CancellationToken cancellation = default


            );
        UniTask UnloadSceneAsync(SceneInstance sceneInstance);
    }
}
