using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Threading;
using TowerDefense.Assets._Project.Scripts.Gameplay.Configs;
using TowerDefense.Assets._Project.Scripts.Utilities;
using TowerDefense.Scripts.Utilities.Core;
using UnityEngine;
using VContainer.Unity;

public class WaveManager
{   
    private readonly EnemySpawner _enemySpawner;

    private WaveConfig _config;
    private readonly IAssetLoader _assetLoader;

    public event Action<int> OnWaveChanged;

    private int _wave = 1;
    private int _enemyToSpawnCount;

    public int Wave => _wave;
    public int MaxWave => _config.MaxWave;
    public int EnemyLeft => _enemyToSpawnCount;

    public WaveManager(EnemySpawner enemySpawner, IAssetLoader assetLoader)
    {
        _enemySpawner = enemySpawner;
        _assetLoader = assetLoader;
    }
    //async UniTask IAsyncStartable.StartAsync(CancellationToken cancellation)
    //{
    //    if (_config == null)
    //        _config = await GetConfig(cancellation);

    //    UpdateWaveAttributes();
    //}

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        _config = await GetConfig(cancellation);
        Debug.Log($"_config.EnemySpawnRate {_config.EnemySpawnRate}");
        UpdateWaveAttributes();
    }

    public async UniTask StartWaveAsync(CancellationToken cancellation = default)
    {
        await WaveLoopAsync(cancellation);
    } 
    private async UniTask WaveLoopAsync(CancellationToken cancellation = default)
    {
        while(_config.MaxWave >= _wave)
        {
            await SpawnEnemyAsync(cancellation);
            await UniTask.WaitForSeconds(_config.WaveCooldown, cancellationToken: cancellation);
            NextWave();
        }
    }

    private async UniTask SpawnEnemyAsync(CancellationToken cancellation)
    {
        while (_enemyToSpawnCount > 0)
        {
            Debug.Log($"WaveManager: wave: {_wave} enemyToSpawn: {_enemyToSpawnCount}" );
            _enemySpawner.SpawnEnemyRandom();
            _enemyToSpawnCount--;
            await UniTask.WaitForSeconds(_config.EnemySpawnRate,cancellationToken: cancellation);
        }
    }

    private void NextWave()
    {
        _wave++;
        UpdateWaveAttributes();
        OnWaveChanged?.Invoke(_wave);
    }

    private void UpdateWaveAttributes()
    {
        _enemyToSpawnCount = UnityEngine.Random.Range(_wave, _wave + 10);
    }

    private async UniTask<WaveConfig> GetConfig(CancellationToken cancellation = default)
    {
        return await _assetLoader.GetAsync<WaveConfig>();
    }
}
