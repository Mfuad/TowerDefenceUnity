using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private float waveCooldown;

    [Header("References")]
    [SerializeField] private EnemySO[] EnemiesSO;
    [SerializeField] private Transform EnemiesFolder;


    public static EnemySpawner Instance { private set; get; }
    private LinkedList<Transform> path;

    public event Action<int> OnWaveChanged;
    public event Action<int> OnEnemyLeftChanged;
    public int Wave { private set; get; }
    public int EnemyLeft { private set; get; }

    private int enemyToSpawnCount;
    private float enemySpawnTimer;
    private float waveCooldownTimer;

    private void Awake()
    {
        Instance = this;
        Wave = 1;
    }

    private void Start()
    {
        path = LevelManager.Instance.Path;
        UpdateWaveAttributes();
    }
    public void OnEnemyDeath()
    {
        EnemyLeft--;
        OnEnemyLeftChanged?.Invoke(EnemyLeft);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning()) return;

        if (enemyToSpawnCount > 0)
        {
            if (enemySpawnTimer >= enemySpawnRate)
            {
                EnemySO enemySO = GetEnemyByRarity();
                Instantiate(enemySO.prefab, path.First.Value.position, new Quaternion(), EnemiesFolder);
                enemyToSpawnCount--;
                enemySpawnTimer = 0;
            }
            enemySpawnTimer += Time.deltaTime;
        }

        if (EnemyLeft <= 0)
        {
            if (waveCooldownTimer >= waveCooldown)
            {
                NextWave();
                waveCooldownTimer = 0;
            }
            waveCooldownTimer += Time.deltaTime;
        }
    }

    private EnemySO GetEnemyByRarity()
    {
        float minRarity = Wave < 100 ? Wave : 100;
        float rarityScore = UnityEngine.Random.Range(minRarity, 101);
        foreach (EnemySO enemySO in EnemiesSO)
        {
            if(enemySO.rarity > rarityScore)
            {
                return enemySO;
            }
        }
        return EnemiesSO[0];
    }
    private void NextWave()
    {
        Wave++;
        UpdateWaveAttributes();
    }

    private void UpdateWaveAttributes()
    {
        enemyToSpawnCount = UnityEngine.Random.Range(Wave, Wave + 10);
        EnemyLeft = enemyToSpawnCount;
        OnWaveChanged?.Invoke(Wave);
        OnEnemyLeftChanged?.Invoke(EnemyLeft);
    }
}
