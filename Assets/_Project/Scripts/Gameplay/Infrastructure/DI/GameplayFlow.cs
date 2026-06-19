using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TowerDefense.Assets._Project.Scripts.Gameplay.Configs;
using TowerDefense.Assets._Project.Scripts.Gameplay.Domain;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace TowerDefense.Assets._Project.Scripts.Gameplay.Infrastructure.DI
{
    public class GameplayFlow : IAsyncStartable
    {
        [Inject] private WaveManager _waveManager;
        [Inject] private LevelManager _levelManager;
        [Inject] private GameManager _gameManager;
        [Inject] private GameInput _gameInput;
        [Inject] private EnemySpawner _enemySpawner;          
        //[Inject] private SceneLoader _sceneLoader;          

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            //await _levelManager.StartAsync(cancellation);
            await _waveManager.StartAsync(cancellation);
            //await _sceneLoader.LoadSceneAsync(_levelManager.CurrentLevel);
            //await _waveManager.StartWaveAsync(cancellation);

        }

    }
}