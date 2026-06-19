using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TowerDefense.Assets._Project.Scripts.Gameplay.Core.Interfaces;
using TowerDefense.Assets._Project.Scripts.Gameplay.Domain;
using TowerDefense.Assets._Project.Scripts.Utilities;
using UnityEngine.Splines;
using VContainer.Unity;

public class EnemySpawner : IStartable,IInitializable, IPathUser
{
    private readonly List<EnemyConfig> EnemiesConfigs;

    private readonly Enemy _prefab;

    private SplineContainer _path;

    private MonoBehaviourPool<Enemy> _enemyPoolManager;
    private ConfigProvider _configProvider;

    public EnemySpawner(ConfigProvider configProvider,LevelManager levelManager)
    {
        _configProvider = configProvider;
    }

    void IInitializable.Initialize()
    {
        _enemyPoolManager = new(_prefab);
    }

    void IStartable.Start()
    { 
    }

    public Enemy SpawnEnemy(EnemyConfig enemyConfig)
    {
        var enemy = _enemyPoolManager.Get();
        enemy.Init(enemyConfig, _path);
        return enemy;
    }

    public Enemy SpawnEnemyRandom()
    {
        EnemyConfig enemyConfig = GetEnemyByRarity();
        return SpawnEnemy(enemyConfig);
    }

    private EnemyConfig GetEnemyByRarity()
    {
        float rarityScore = UnityEngine.Random.Range(0, 101);
        foreach (EnemyConfig enemyConfig in EnemiesConfigs)
        {
            if(enemyConfig.Rarity <  rarityScore)
            {
                return enemyConfig;
            }
        }
        return EnemiesConfigs[0];
    }

    public void RegisterPath(SplineContainer path)
    {
        _path = path;
    }

    public void UnregisterPath()
    {
        _path = null;
    }
}
