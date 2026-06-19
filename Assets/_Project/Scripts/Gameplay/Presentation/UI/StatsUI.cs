using TMPro;
using UnityEngine;

 class StatsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemyLeftText;

    private int maxHealth;

    private void Start()
    {
        //LevelManager.Instance.OnBalanceChanged += Instance_OnBalanceChanged;
        //Castle.Instance.OnTakeDamage += Castle_OnTakeDamage;
        //EnemySpawner.Instance.OnWaveChanged += EnemySpawner_OnWaveChanged;
        //EnemySpawner.Instance.OnEnemyLeftChanged += EnemySpawner_OnEnemyLeftChanged;

        //maxHealth = LevelManager.Instance.MaxHealth;

        //waveText.text = $"Wave: {EnemySpawner.Instance.Wave}";
        //healthText.text = $"Health: {LevelManager.Instance.Health}/{LevelManager.Instance.MaxHealth}";
        //enemyLeftText.text = $"Enemies: {EnemySpawner.Instance.EnemyLeft}";
        //goldText.text = $"Gold: {LevelManager.Instance.Balance}";

    }


    private void OnDestroy()
    {
        //LevelManager.Instance.OnBalanceChanged -= Instance_OnBalanceChanged;
        //Castle.Instance.OnTakeDamage -= Castle_OnTakeDamage;
        //EnemySpawner.Instance.OnWaveChanged -= EnemySpawner_OnWaveChanged;
        //EnemySpawner.Instance.OnEnemyLeftChanged -= EnemySpawner_OnEnemyLeftChanged;
    }

    private void EnemySpawner_OnWaveChanged(int wave)
    {
        waveText.text = $"Wave: {wave}";
    }
    private void Castle_OnTakeDamage(int health)
    {
        healthText.text = $"Health: {health}/{maxHealth}";
    }
    private void EnemySpawner_OnEnemyLeftChanged(int enemyLeft)
    {
        enemyLeftText.text = $"Enemies: {enemyLeft}";
    }

    private void Instance_OnBalanceChanged(int balance)
    {
        goldText.text = $"Gold: {balance}";
    }
}
